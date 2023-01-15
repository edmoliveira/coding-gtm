import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReadRequest } from '../services/product-price/read/models/request/read-request';
import { ReadResponse } from '../services/product-price/read/models/response/read-response';
import { ReadService } from '../services/product-price/read/read-service';
import { switchMap, Observable } from "rxjs";
import { CountryProductPriceModel } from '../services/product-price/read/models/country-product-price-model';
import { RateCountryProductPriceModel } from '../services/product-price/read/models/rate-country-product-price-model';
import { SaveService } from '../services/product-price/save/save-service';
import { SaveRequest } from '../services/product-price/save/models/request/save-request';
import { RatePriceModel } from '../services/product-price/save/models/rate-price-model';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  productPriceResponse: Observable<ReadResponse>;
  formGroup: FormGroup;

  rates: RateCountryProductPriceModel[] = [];

  private countryIdSelected: number;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private readPriceService: ReadService,
    private savePriceService: SaveService) { }

  ngOnInit(): void {
    const numRegex = /^-?\d*[.,]?\d{0,2}$/;

    this.formGroup = new FormGroup({
      countryId: new FormControl(),
      rate: new FormControl(null, [Validators.required]),
      priceWithoutVAT: new FormControl(null, [Validators.required, Validators.pattern(numRegex), Validators.min(1)]),
      vat: new FormControl(null),
      priceInclVAT: new FormControl(null, [Validators.required, Validators.pattern(numRegex), Validators.min(1)]),
    });

    this.formGroup.controls.countryId.valueChanges.subscribe({
      next: (value: CountryProductPriceModel) => {
        if(value != null) {
          this.countryIdSelected = value.countryId;
          this.rates = value.rates;
          this.formGroup.controls.priceWithoutVAT.setValue(value.priceWithoutVAT);
          this.formGroup.controls.vat.setValue(value.vat);
          this.formGroup.controls.priceInclVAT.setValue(value.priceInclVAT);
        }
        else {
          this.countryIdSelected = null;
          this.rates = [];
          this.formGroup.controls.rate.setValue(null);
          this.formGroup.controls.priceWithoutVAT.setValue(null);
          this.formGroup.controls.vat.setValue(null);
          this.formGroup.controls.priceInclVAT.setValue(null);
        }
      }
    });

    function calculateVAT(controls: any) {
      const rate = parseFloat(controls.rate.value);
      const priceWithoutVAT = parseFloat(controls.priceWithoutVAT.value);

      if(rate > 0 && priceWithoutVAT > 0) {
        const vat = Math.round(priceWithoutVAT * rate / 100);
        const priceInclVAT = +((priceWithoutVAT + vat).toFixed(2));

        controls.vat.setValue(vat, { emitEvent: false });
        controls.priceInclVAT.setValue(priceInclVAT, { emitEvent: false });
      }
    }

    function calculateVAT2(controls: any) {
      const rate = parseFloat(controls.rate.value);
      const priceInclVAT = parseFloat(controls.priceInclVAT.value);

      if(rate > 0 && priceInclVAT > 0) {
        const priceWithoutVAT = +((priceInclVAT / (1 + rate / 100)).toFixed(2));
        const vat = Math.round(priceWithoutVAT * rate / 100);

        controls.vat.setValue(vat, { emitEvent: false });
        controls.priceWithoutVAT.setValue(priceWithoutVAT, { emitEvent: false });
      }
    }

    this.formGroup.controls.rate.valueChanges.subscribe({
      next: () => {
        calculateVAT(this.formGroup.controls);
      }
    });

    this.formGroup.controls.priceWithoutVAT.valueChanges.subscribe({
      next: () => {
        calculateVAT(this.formGroup.controls);
      }
    });

    this.formGroup.controls.priceInclVAT.valueChanges.subscribe({
      next: () => {
        calculateVAT2(this.formGroup.controls);
      }
    });

    this.productPriceResponse = this.route.params.pipe(
      switchMap(params => {
        const request = new ReadRequest();

        request.productId = +params['id'];

        return this.readPriceService.read(request);
      })
    );
  }

  onSubmit(productId:number, countries: CountryProductPriceModel[]) {
    const country: CountryProductPriceModel = countries.find(c => c.countryId === this.countryIdSelected);

    const rateValue = this.formGroup.controls.rate.value;
    country.priceWithoutVAT = parseFloat(this.formGroup.controls.priceWithoutVAT.value);
    country.isSaved = true;

    country.rates.forEach(item => {
      item.isSaved = item.rate == rateValue;
    })

    const request = new SaveRequest();

    request.productId = productId;
    request.rates = [];

    countries.filter(c => c.isSaved).forEach(item => {
      request.rates.push({
        countryId: item.countryId,
        price: item.priceWithoutVAT,
        rate: item.rates.find(r => r.isSaved).rate
      });
    });

    this.savePriceService.save(request).subscribe({
      next: () => {
        this.formGroup.reset();
        this.formGroup.markAsPristine();
        this.formGroup.markAsUntouched();
      },
      error: () => { alert("There was a problem with your request.") }
   })
  }

  cancel() {
    this.router.navigate(['/']);
  }
}

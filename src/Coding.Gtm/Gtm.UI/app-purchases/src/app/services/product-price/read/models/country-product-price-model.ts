import { RateCountryProductPriceModel } from "./rate-country-product-price-model";

export class CountryProductPriceModel {
	countryId: number;
    countrytName: string;
    isSaved: boolean;
    priceWithoutVAT: number;
    vat: number;
    priceInclVAT: number;
    rates: RateCountryProductPriceModel[]
}
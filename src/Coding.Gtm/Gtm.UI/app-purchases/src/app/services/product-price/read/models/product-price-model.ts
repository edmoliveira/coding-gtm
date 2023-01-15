import { CountryProductPriceModel } from "./country-product-price-model";

export class ProductPriceModel {
    productId: number;
    productName: string;
    countries: CountryProductPriceModel[];
}
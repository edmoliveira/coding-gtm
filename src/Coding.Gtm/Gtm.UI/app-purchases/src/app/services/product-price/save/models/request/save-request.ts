import { RatePriceModel } from "../rate-price-model";

export class SaveRequest {
    productId: number;
    rates: RatePriceModel[]
}
import {BaseRS} from "./BaseRS";
import {ProductRS} from "./ProductRS";

export interface OrderItemRS extends BaseRS{
    id: string;
    creationTime: Date,
    orderId: string;
    productId: string;
    product: ProductRS;
}
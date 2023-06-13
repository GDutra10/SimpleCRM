import {BaseRS} from "./BaseRS";
import {OrderItemRS} from "./OrderItemRS";
export interface OrderRS extends BaseRS{
    id: string;
    creationTime: Date,
    interactionId: string;
    orderItems: OrderItemRS[];
}
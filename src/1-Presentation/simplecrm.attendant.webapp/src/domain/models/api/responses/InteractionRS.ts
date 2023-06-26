import {UserRS} from "./UserRS";
import {CustomerRS} from "./CustomerRS";
import {BaseRS} from "./BaseRS";

export interface InteractionRS extends BaseRS{
    id: string,
    userId: string,
    customerId: string,
    creationTime: Date,
    endTime: Date,
    user: UserRS,
    customer: CustomerRS
}
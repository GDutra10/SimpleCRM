import {InteractionState} from "../InteractionState";
import {BaseRS} from "./BaseRS";

export interface CustomerRS extends BaseRS{
    id: string,
    creationTime: Date,
    name: string,
    email: string,
    telephone: string,
    state: InteractionState
}
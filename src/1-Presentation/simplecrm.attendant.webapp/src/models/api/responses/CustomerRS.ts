import {BaseRS} from "./BaseRS";
import {InteractionState} from "../InteractionState";

export interface CustomerRS extends BaseRS{
    id: string;
    creationTime: Date;
    name: string;
    email: string;
    telephone: string;
    state: InteractionState | null;
}
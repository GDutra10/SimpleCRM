import {BaseRS} from "./BaseRS";

export interface ProductRS extends BaseRS{
    id: string,
    name: string,
    active: boolean
}
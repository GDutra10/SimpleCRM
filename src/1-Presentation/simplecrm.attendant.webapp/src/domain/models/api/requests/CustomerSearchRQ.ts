import {BaseSearchRQ} from "./BaseSearchRQ";

export interface CustomerSearchRQ extends BaseSearchRQ {
    name: string;
    email: string;
    telephone: string;
}
import {BaseSearchRQ} from "./BaseSearchRQ";

export interface ProductSearchRQ extends BaseSearchRQ{
    onlyActive: boolean
}
import {BaseRS} from "./BaseRS";

export interface BaseSearchRS<T> extends BaseRS { 
    records: T[];
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalRecords: number;
}
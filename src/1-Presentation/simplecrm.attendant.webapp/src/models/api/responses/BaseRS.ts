import { Validation } from "../Validation";

export interface BaseRS {
    error: string | null;
    validations: Array<Validation> | null;
}
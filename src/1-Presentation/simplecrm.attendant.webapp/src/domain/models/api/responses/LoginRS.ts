import { BaseRS } from "./BaseRS";

export interface LoginRS extends BaseRS {
    accessToken: string;
    expiresIn: number;
}
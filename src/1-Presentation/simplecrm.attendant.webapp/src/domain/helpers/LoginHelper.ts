import {SessionConstants} from "../constants/SessionConstants";

export class LoginHelper{
    static Logout() : void {
        sessionStorage.removeItem(SessionConstants.AccessToken);
        sessionStorage.removeItem(SessionConstants.ExpiresIn);
        window.location.reload();
    }
}
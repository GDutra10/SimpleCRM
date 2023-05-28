import {SessionConstants} from "../constants/SessionConstants";

export class SimpleCRMWebAPI {
    protected readonly baseUrl: string = "https://localhost:44312";
    
    async executeAsync<T>(httpMethod: HttpMethod, endpoint: string, body: any | null = null, accessToken: string | null = null) : Promise<T> {
        
        try{
            const init: RequestInit = { 
                method: httpMethod.toString(),
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": (accessToken === null) ? "" : `Bearer ${accessToken}`
                }
            };

            if (body != null)
                init.body = JSON.stringify(body);

            const response = await fetch(`${this.baseUrl}${endpoint}`, init);
            
            if ((response.status >= 200 && response.status <= 299) ||
                response.status === 400 || response.status === 500)
                return Promise.resolve<T>(await response.json());
            
            if (response.status === 401){
                sessionStorage.removeItem(SessionConstants.AccessToken);
                sessionStorage.removeItem(SessionConstants.ExpiresIn);
                window.location.reload();
            }
                
            
            return { error: "happened something wrong, please try again later"} as T;
        } catch (error: any){
            return { error: `happened something wrong, please try again later.${(process.env.NODE_ENV === "development") ? "\r\n" + error.message : ""}`} as T;
        }
    }
}

export enum HttpMethod { 
    Get = "GET",
    Post = "POST",
    Put = "PUT",
    Delete = "DELETE"
}
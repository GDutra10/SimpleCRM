export enum AuthenticationEndpoint {
    Login = "/Authentication/Login",
    ValidateToken = "/Authentication/ValidateToken"
}

export enum CustomerEndpoint {
    Customers = "/Attendant/Customers"
}

export enum InteractionEndpoint {
    Start = "/Attendant/Interactions/Start",
    Finish = "/Attendant/Interactions/Finish",
    Order = "/Attendant/Interactions/Order"
}

export enum ProductEndpoint {
    Products = "/Common/Products"
}
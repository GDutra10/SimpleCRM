import React, { useState } from 'react';
import {HttpMethod, SimpleCRMWebAPI} from "../../api/SimpleCRMWebAPI";
import {LoginRS} from "../../models/api/responses/LoginRS";
import {LoginRQ} from "../../models/api/requests/LoginRQ";
import {SessionConstants} from "../../constants/SessionConstants";
import Control from "../../components/common/Control";

function Login() {
    let [email, setEmail] = useState<string>("");
    let [password, setPassword] = useState<string>("");
    let [emailValidation, setEmailValidation] = useState<string>("");
    let [passwordValidation, setPasswordValidation] = useState<string>("");
    let [globalValidation, setGlobalValidation] = useState<string>("");
    
    async function tryLogin(event: React.MouseEvent<HTMLButtonElement>): Promise<any> {
        (event.target as HTMLButtonElement).disabled = true;
        setEmailValidation("");
        setPasswordValidation("");
        setGlobalValidation("");
        
        const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
        const loginRQ : LoginRQ = { email: email, password: password };
        const loginRS : LoginRS = await api.executeAsync<LoginRS>(HttpMethod.Post, "/Authentication/Login", loginRQ);
        
        console.log(loginRS);
        
        if (!loginRS){
            (event.target as HTMLButtonElement).disabled = false;
            return;
        }
        
        if (loginRS.error && loginRS.error.length > 0){
            alert(loginRS.error);
            (event.target as HTMLButtonElement).disabled = false;
            return;
        }

        if (loginRS.validations && loginRS.validations.length > 0) {
            loginRS.validations.forEach(v => {
                if (v.field === "Email")
                    setEmailValidation(v.message);
                else if (v.field === "Password")
                    setPasswordValidation(v.message);
                else if (v.field === "")
                    setGlobalValidation(v.message);
            });

            (event.target as HTMLButtonElement).disabled = false;
            return;
        }

        sessionStorage.setItem(SessionConstants.AccessToken, loginRS.accessToken);
        sessionStorage.setItem(SessionConstants.ExpiresIn, loginRS.expiresIn.toString());
        (event.target as HTMLButtonElement).disabled = false;
        window.location.reload();
    }
    
    return <>
        <div className="form">
            <Control 
                label="Email" 
                type="text" 
                value={email} 
                error={emailValidation}
                onChange={async event => setEmail(event.target.value) } 
                required></Control>
            <Control 
                label="Password" 
                type="password" 
                value={password} 
                error={passwordValidation} 
                onChange={async event => setPassword(event.target.value)} 
                required></Control>
            <label>{globalValidation}</label>
            
            <button onClick={async event => await tryLogin(event)}>Login</button>
        </div>
    </>;
}

export default Login;
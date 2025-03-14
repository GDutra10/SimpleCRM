import React from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";
// import logo from './logo.svg';
import './App.css';
import {SessionConstants} from "./domain/constants/SessionConstants";
import {HttpMethod, SimpleCRMWebAPI} from "./infra/api/SimpleCRMWebAPI";
import Layout from "./presentation/pages/Layout";
import Login from "./presentation/pages/Login/Index";
import Home from './presentation/pages/Home/Index';
import NotFound from "./presentation/pages/404/Index";
import {InteractionProvider} from "./presentation/contexts/InteractionContext";
import {AuthenticationEndpoint} from "./domain/constants/EndpointConstants";
import {ModalProvider} from "./presentation/contexts/ModalContext";
import {Logger} from "./infra/logger/Logger";

function App() {
    Logger.logInfo("------------------------");
    Logger.logInfo("Stating app");
    const accessToken = sessionStorage.getItem(SessionConstants.AccessToken);
  
    if (!accessToken){
        Logger.logInfo("access token null, showing login page...");
        return <Login></Login>;
    }

    Logger.logInfo("checking the access token");
    new SimpleCRMWebAPI().executeAsync<boolean>(HttpMethod.Post, AuthenticationEndpoint.ValidateToken, null, true);

    return (
        <BrowserRouter>
            <ModalProvider>
                <InteractionProvider>
                    <Routes>
                        <Route path="/" element={<Layout/>}>
                            <Route path="/" element={<Home/>}></Route>
                            <Route path="/404" element={<NotFound/>}></Route>
                        </Route>
                    </Routes>
                </InteractionProvider>
            </ModalProvider>
        </BrowserRouter>
    );
}

export default App;

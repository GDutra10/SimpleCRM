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
import Interaction from "./presentation/pages/Interaction/Interaction";
import {InteractionRSProvider} from "./presentation/contexts/InteractionRSContext";
import {AuthenticationEndpoint} from "./domain/constants/EndpointConstants";
import {ModalProvider} from "./presentation/contexts/ModalContext";
import {Logger} from "./infra/logger/Logger";

function App() {
    Logger.logDebug("------------------------");
    Logger.logDebug("Stating app");
    const accessToken = sessionStorage.getItem(SessionConstants.AccessToken);
  
    if (!accessToken){
        Logger.logDebug("access token null, showing login page...");
        return <Login></Login>;
    }

    Logger.logDebug("checking the access token");
    new SimpleCRMWebAPI().executeAsync<boolean>(HttpMethod.Post, AuthenticationEndpoint.ValidateToken, null, true)
  
    return (
      <>
          <BrowserRouter>
              <ModalProvider>
                  <InteractionRSProvider>
                      <Routes>
                          <Route path="/" element={<Layout/>}>
                              <Route path="/" element={<Home/>}></Route>
                              <Route path="/interaction/:customerId" element={<Interaction/>}></Route>
                              <Route path="/404" element={<NotFound/>}></Route>
                          </Route>
                      </Routes>
                  </InteractionRSProvider>
              </ModalProvider>
          </BrowserRouter>
      </>
  );
}

export default App;

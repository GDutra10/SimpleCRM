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

function App() {
  const accessToken = sessionStorage.getItem(SessionConstants.AccessToken);
  
  if (!accessToken)
    return <Login></Login>;

  new SimpleCRMWebAPI().executeAsync<boolean>(HttpMethod.Post, "/Authentication/ValidateToken", null, true)
  
  return (
      <>
          <BrowserRouter>
              <InteractionRSProvider>
                  <Routes>
                      <Route path="/" element={<Layout/>}>
                          <Route path="/" element={<Home/>}></Route>
                          <Route path="/interaction/:customerId" element={<Interaction/>}></Route>
                          <Route path="/404" element={<NotFound/>}></Route>
                      </Route>
                  </Routes>
              </InteractionRSProvider>
          </BrowserRouter>
      </>
  );
}

export default App;

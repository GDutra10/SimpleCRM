import React from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";
// import logo from './logo.svg';
import './App.css';
import {SessionConstants} from "./constants/SessionConstants";
import {HttpMethod, SimpleCRMWebAPI} from "./api/SimpleCRMWebAPI";
import Layout from "./pages/Layout";
import Login from "./pages/Login/Index";
import Home from './pages/Home/Index';
import NotFound from "./pages/404/Index";
import Interaction from "./pages/Interaction/Interaction";

function App() {
  const accessToken = sessionStorage.getItem(SessionConstants.AccessToken);
  
  if (!accessToken)
    return <Login></Login>;

  new SimpleCRMWebAPI().executeAsync<boolean>(HttpMethod.Post, "/Authentication/ValidateToken", null, true)
  
  return (
      <>
          <BrowserRouter>
              <Routes>
                  <Route path="/" element={<Layout/>}>
                      <Route path="/" element={<Home/>}></Route>
                      <Route path="/interaction/:idCustomer" element={<Interaction/>}></Route>
                      <Route path="/404" element={<NotFound/>}></Route>
                  </Route>
              </Routes>
          </BrowserRouter>
      </>
  );
}

export default App;

﻿using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.Backoffice.WebApp.Controllers;

public class BaseController : Controller
{
    protected bool ValidateResponse<T>((T?, ValidationRS?, ErrorRS?) response)
    {
        if (response.Item2 is null && response.Item3 is null) 
            return true;

        if (response.Item2 is not null)
            ViewBag.Validations = response.Item2;

        if (response.Item3 is not null)
            ViewBag.Error = response.Item3.Error;

        return false;
    }
}
﻿using System;
using IdentityServer4.Models;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Utils
{
    public static class ControllerExtensions
    {
            /// <summary>
            /// Checks if the redirect URI is for a native client.
            /// </summary>
            /// <returns></returns>
            public static bool IsNativeClient(this AuthorizationRequest context)
            {
                return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
                       && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
            }

            public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
            {
                controller.HttpContext.Response.StatusCode = 200;
                controller.HttpContext.Response.Headers["Location"] = "";
            
                return controller.View(viewName, new RedirectViewModel { RedirectUrl = redirectUri });
            }
        
    }
}
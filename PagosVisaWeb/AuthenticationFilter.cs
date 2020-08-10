using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PagosVisaWeb
{
    /*******************************************************************************************
* SendEmailOutlook
* Para en envió de correos
* Programador: Rodercik Cusirramos Montesinos
* Fecha de creacion: 22/06/2020
* Fecha de modificacion: 03/08/2020      
* *****************************************************************************************/

    public class AuthenticationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var path = context.HttpContext.Request.Path;

            List<String> path_anonymus = new List<string>();
            path_anonymus.Add("/Login");
            path_anonymus.Add("/Salir");
            path_anonymus.Add("/RecuperarContrasena");
            path_anonymus.Add("/Registro");
            path_anonymus.Add("/confirmar");
            path_anonymus.Add("/Autenticacion/PaginaNoEncontrada");
            path_anonymus.Add("/get-captcha-image");

            String anonymus = path_anonymus.Where(m => m.Equals(path)).FirstOrDefault();
            if (anonymus == null)
            {
                if (String.IsNullOrEmpty(context.HttpContext.Session.GetString("USRidUsuario")))
                {
                    context.Result = new RedirectToRouteResult(
                                new RouteValueDictionary(new { controller = "Autenticacion", action = "Login" }));
                }
            } 
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
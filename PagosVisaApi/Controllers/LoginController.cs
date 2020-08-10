using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Electrosur.Helper;
using Electrosur.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PagosVisaApi.Helper;
using PagosVisaApi.Models;

namespace Electrosur.Controllers
{
    /*******************************************************************************************
* PagosVisaWeb
* Este controlador es para ingresar a la aplicación
* Programador: Rodercik Cusirramos Montesinos
* Fecha de creacion: 22/06/2020
* Fecha de modificacion: 03/08/2020      
* *****************************************************************************************/

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _service;
        private readonly ElectrosurContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(ElectrosurContext context, ILoginService service, IConfiguration configuration)
        {
            _context = context;
            _service = service;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index([FromBody] FormRequest d)
        {
            Cryptography crypto = new Cryptography();
            String jsonString = crypto.Decrypt(d.d, _configuration["ClaveSecretaMovil"]);

            FormLogin user = JsonSerializer.Deserialize<FormLogin>(jsonString);

            IActionResult response = Unauthorized();
            try
            {
                user.Password = PasswordHash.GetMd5Hash(user.Password);

                var usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrcorreoPrimario == user.UserName &&
                       m.Usrcontrasena == user.Password).FirstOrDefault();

                if (usuario != null)
                {
                    if (usuario.UsrconfirmacionCorreo)
                    {
                        var tokenString = _service.GenerateJWT(user);

                        ResponseLogin responseLogin = new ResponseLogin();
                        responseLogin.Token = tokenString;
                        responseLogin.USRNombre = usuario.Usrnombre + " " + usuario.UsrapellidoPaterno + " " + usuario.UsrapellidoMaterno;
                        responseLogin.UsruniqueId = usuario.UsruniqueId.ToString();

                        String d1 = JsonSerializer.Serialize(responseLogin);

                        response = Ok(new
                        {
                            Estado = "OK",
                            d = crypto.Encrypt(d1, _configuration["ClaveSecretaMovil"])
                        //Token = tokenString,
                        //USRNombre = usuario.Usrnombre + " " + usuario.UsrapellidoPaterno + " " + usuario.UsrapellidoMaterno,
                        //UsruniqueId = usuario.UsruniqueId.ToString()
                    });
                    }
                    else
                    {
                        response = Ok(new
                        {
                            Estado = "Error",
                            Mensaje = "Correo no confirmado",
                        });
                    }
                }
                else
                {
                    response = Ok(new
                    {
                        Estado = "Error",
                        Mensaje = "Email o contraseña invalidos",
                    });
                }
            }
            catch (Exception ex)
            {
                response = Ok(new
                {
                    Estado = "Error",
                    Mensaje = ex.Message,
                });
            }
            return response;
        }


        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Index([FromBody] FormLogin user)
        //{
        //    IActionResult response = Unauthorized();
        //    try
        //    {
        //        user.Password = PasswordHash.GetMd5Hash(user.Password);

        //        var usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrcorreoPrimario == user.UserName &&
        //               m.Usrcontrasena == user.Password).FirstOrDefault();

        //        if (usuario != null)
        //        {
        //            if (usuario.UsrconfirmacionCorreo)
        //            {
        //                var tokenString = _service.GenerateJWT(user);
        //                response = Ok(new
        //                {
        //                    Estado = "OK",
        //                    Token = tokenString,
        //                    USRNombre = usuario.Usrnombre + " " + usuario.UsrapellidoPaterno + " " + usuario.UsrapellidoMaterno,
        //                    UsruniqueId = usuario.UsruniqueId.ToString()
        //                });
        //            }
        //            else
        //            {
        //                response = Ok(new
        //                {
        //                    Estado = "Error",
        //                    Mensaje = "Correo no confirmado",
        //                });
        //            }
        //        }
        //        else
        //        {
        //            response = Ok(new
        //            {
        //                Estado = "Error",
        //                Mensaje = "Email o contraseña invalidos",
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = Ok(new
        //        {
        //            Estado = "Error",
        //            Mensaje = ex.Message,
        //        });
        //    }
        //    return response;
        //}
    }
}

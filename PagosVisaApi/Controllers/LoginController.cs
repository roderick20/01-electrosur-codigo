using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electrosur.Helper;
using Electrosur.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electrosur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _service;
        private readonly ElectrosurContext _context;

        public LoginController(ElectrosurContext context, ILoginService service)
        {
            _context = context;
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index([FromBody] FormLogin user)
        {
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
                        response = Ok(new
                        {
                            Estado = "OK",
                            Token = tokenString,
                            USRNombre = usuario.Usrnombre + " " + usuario.UsrapellidoPaterno + " " + usuario.UsrapellidoMaterno,
                            UsruniqueId = usuario.UsruniqueId.ToString()
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
    }
}

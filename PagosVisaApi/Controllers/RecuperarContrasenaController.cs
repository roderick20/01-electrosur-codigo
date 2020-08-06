using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Electrosur.Helper;
using Electrosur.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Electrosur.Controllers
{
    /*******************************************************************************************
* PagosVisaWeb
* Este controlador es para recuperar contraseña
* Programador: Rodercik Cusirramos Montesinos
* Fecha de creacion: 22/06/2020
* Fecha de modificacion: 03/08/2020      
* *****************************************************************************************/

    [Route("api/[controller]")]
    [ApiController]
    public class RecuperarContrasenaController : ControllerBase
    {
        private IPrincipal _principal;
        private readonly ElectrosurContext _context;
        private readonly IWebHostEnvironment _env;

        public RecuperarContrasenaController(ElectrosurContext context, IPrincipal principal, IWebHostEnvironment env)
        {
            _context = context;
            _principal = principal;
            _env = env;
        }

        [HttpPost]
        public IActionResult Index([FromBody] FormRecuperarContrasena recuperar)
        {
            IActionResult response = Unauthorized();
            try
            {
                var usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrcorreoPrimario == recuperar.correo.Trim()).FirstOrDefault();

                if (usuario != null)
                {
                    String nuevo_password = GenerateCaptchaCode();
                    usuario.Usrcontrasena = PasswordHash.GetMd5Hash(nuevo_password);
                    usuario.Usrmodificado = DateTime.Now;
                    usuario.UsrultimoAcceso = DateTime.Now;
                    _context.Update(usuario);
                    _context.SaveChanges();

                    var path = _env.WebRootPath + "/plantilla_email/recuperarcontrasena_email.html";
                    String fileContents = System.IO.File.ReadAllText(path);
                    fileContents = fileContents.Replace("$$NuevaContrasena$$", nuevo_password);

                    SendEmailOutlook email_obj = new SendEmailOutlook(usuario.UsrcorreoPrimario, "Electrosur confirmacion de correo", fileContents, _env.WebRootPath);
                    email_obj.Send();

                    response = Ok(new { Estado = "OK" });
                }
                else
                {

                    response = Ok(new
                    {
                        Estado = "Error",
                        Mensaje = "Error correo no existe : " + recuperar.correo
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

        public string GenerateCaptchaCode()
        {
            string Letters = "2346789ABCDEFGHJKLMNPRTUVWXYZ";

            Random rand = new Random();
            int maxRand = Letters.Length - 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                int index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }
    }
}

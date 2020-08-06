using Electrosur.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Electrosur.Helper;
using Microsoft.AspNetCore.Hosting;

namespace Electrosur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarseController : ControllerBase
    {
        private ILoginService _service;
        private readonly ElectrosurContext _context;
        private readonly IWebHostEnvironment _env;

        public RegistrarseController(ElectrosurContext context, ILoginService service, IWebHostEnvironment env)
        {
            _context = context;
            _service = service;
            _env = env;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index([FromBody] FormRegistro registro)
        {
            IActionResult response = Unauthorized();
            try
            {
                var usuariorepetido = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrnumeroDocumento == registro.numeroDocumento ||
                    m.UsrcorreoPrimario == registro.correoPrimario).FirstOrDefault();

                if (usuariorepetido != null)
                {
                    String error = "";
                    if (usuariorepetido.UsrnumeroDocumento == registro.numeroDocumento)
                    {
                        error = "Número de documento repetido";
                    }

                    if (usuariorepetido.UsrcorreoPrimario == registro.correoPrimario)
                    {
                        error = "Correo primario repetido";
                    }


                    response = Ok(new
                    {
                        Estado = "Error",
                        Mensaje = error
                    });
                }
                else
                {

                    PdpUsrtUsuarioDelSistema usuario = new PdpUsrtUsuarioDelSistema();

                    usuario.UsruniqueId = Guid.NewGuid();
                    usuario.UsrtipoDocumento = registro.tipoDocumento;
                    usuario.UsrnumeroDocumento = registro.numeroDocumento;
                    usuario.Usrnombre = registro.nombres;
                    usuario.UsrapellidoPaterno = registro.apellidoPaterno;
                    usuario.UsrapellidoMaterno = registro.apellidoMaterno;
                    usuario.UsrcorreoPrimario = registro.correoPrimario;
                    usuario.UsrcorreoSecundario = registro.correoSecundario;
                    usuario.Usrtelefono = registro.numeroTelefono;
                    usuario.Usrcontrasena = registro.contrasena;
                    usuario.Usrestado = false;
                    usuario.Usrcreado = DateTime.Now;
                    usuario.Usrmodificado = DateTime.Now;
                    usuario.UsrultimoAcceso = DateTime.Now;
                    usuario.UsrconfirmacionCorreo = false;
                    usuario.UsrrecuperarContrasena = false;

                    _context.Add(usuario);
                    _context.SaveChanges();

                    var path = _env.WebRootPath + "/plantilla_email/confirmacion_email.html";
                    String fileContents = System.IO.File.ReadAllText(path);
                    fileContents = fileContents.Replace("$$UniqueId$$", usuario.UsruniqueId.ToString());

                    SendEmailOutlook email = new SendEmailOutlook(usuario.UsrcorreoPrimario, "Electrosur confirmacion de correo", fileContents, _env.WebRootPath);
                    email.Send();

                    response = Ok(new
                    {
                        Estado = "OK",
                        Mensaje = "",
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

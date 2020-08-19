using Electrosur.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using PagosVisaApi.Models;
using Electrosur.Helper;

namespace Electrosur.Controllers
{
    /*******************************************************************************************
 * PagosVisaWeb
 * Este controlador para el cambio de contraseña
 * Programador: Rodercik Cusirramos Montesinos
 * Fecha de creacion: 22/06/2020
 * Fecha de modificacion: 03/08/2020      
 * *****************************************************************************************/

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CambioContrasenaController : ControllerBase
    {
        private IPrincipal _principal;
        private readonly ElectrosurContext _context;

        public CambioContrasenaController(ElectrosurContext context, IPrincipal principal)
        {
            _context = context;
            _principal = principal;
        }

        [HttpPost]
        public IActionResult Index([FromBody] CambioContrasena recuperar)
        {
            IActionResult response = Unauthorized();
            try
            {
                var uniqueid = new Guid(recuperar.usruniqueid);
                var usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsruniqueId == uniqueid).FirstOrDefault();
                usuario.Usrcontrasena = PasswordHash.GetMd5Hash(recuperar.contrasena);
                usuario.Usrmodificado = DateTime.Now;
                usuario.UsrultimoAcceso = DateTime.Now;
                _context.Update(usuario);
                _context.SaveChanges();
                response = Ok(new { Estado = "OK" });
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

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

namespace Electrosur.Controllers
{
    /*******************************************************************************************
* PagosVisaWeb
* Este controlador es para listar el historial de pago
* Programador: Rodercik Cusirramos Montesinos
* Fecha de creacion: 22/06/2020
* Fecha de modificacion: 03/08/2020      
* *****************************************************************************************/

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HistorialController : ControllerBase
    {
        private IPrincipal _principal;
        private readonly ElectrosurContext _context;
        public HistorialController(ElectrosurContext context, IPrincipal principal)
        {
            _context = context;
            _principal = principal;
        }

        [HttpPost]
        public IActionResult Index([FromBody] FormHistorial pago)
        {
            IActionResult response = Unauthorized();
            try
            {
                int anyo = Convert.ToInt32(pago.anyo);
                int mes = Convert.ToInt32(pago.mes);

                List<Recibos> recibos = new List<Recibos>();
                var usrUniqueId = new Guid(pago.usruniqueid);
                var list = _context.PdpPagpPago
                    .Where(m => m.UsridUsuarioNavigation.UsruniqueId == usrUniqueId &&
                                m.Pagcreado.Year == anyo && 
                                m.Pagcreado.Month == mes)
                    .OrderByDescending(m => m.Pagcreado);

                response = Ok(new { Estado = "OK", recibos = list });
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

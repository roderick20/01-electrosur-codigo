using Electrosur.Helper;
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
using WSSielse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PagosVisaApi.Helper;

namespace Electrosur.Controllers
{
    /*******************************************************************************************
* PagosVisaWeb
* Este controlador es para actualizar el pago de invitado
* Programador: Rodercik Cusirramos Montesinos
* Fecha de creacion: 22/06/2020
* Fecha de modificacion: 03/08/2020      
* *****************************************************************************************/

    [Route("api/[controller]")]
    [ApiController]
    public class PagoActualizarInvitadoController : ControllerBase
    {
        private IPrincipal _principal;
        private readonly ElectrosurContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public PagoActualizarInvitadoController(
            ElectrosurContext context,
            IPrincipal principal,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _context = context;
            _principal = principal;
            _env = env;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Index([FromBody] FormPagoActualizarInvitado pagoActualizar)
        {
            IActionResult response = Unauthorized();

            try
            {
                SCobranzaClient sCobranzaClient = new SCobranzaClient();
                ClsPagoDeudaPeticion clsPagoDeudaPeticion = new ClsPagoDeudaPeticion();
                Cryptography crypto = new Cryptography();

                clsPagoDeudaPeticion.IdentificadorTransaccion = crypto.Decrypt(crypto.Decrypt(pagoActualizar.identificadorintidadionsulta, _configuration["ClaveSecreta"]), _configuration["ClaveSecretaMovil"]);
                clsPagoDeudaPeticion.IdentificadorEntidadPago = Guid.NewGuid().ToString();
                clsPagoDeudaPeticion.AgenciaEntidadPago = _configuration["WebServices:AgenciaEntidadConsulta"];
                clsPagoDeudaPeticion.MedioPagoEntidadPago = _configuration["WebServices:MedioPagoEntidadConsulta"];
                clsPagoDeudaPeticion.FechaPago = DateTime.Now;
                clsPagoDeudaPeticion.UsuarioPago = _configuration["WebServices:UsuarioConsulta"];
                clsPagoDeudaPeticion.ClaveUsuario = _configuration["WebServices:ClaveUsuario"];
                clsPagoDeudaPeticion.CodigoComprobante = pagoActualizar.pagcodigoComprobante;
                clsPagoDeudaPeticion.CodigoSuministro = pagoActualizar.opesuministro;
                clsPagoDeudaPeticion.MontoPago = Convert.ToDecimal(pagoActualizar.opemonto);
                var clsPagoDeudaRespuesta = sCobranzaClient.PagoDeuda(clsPagoDeudaPeticion);

                try
                {
                    var usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrnumeroDocumento == pagoActualizar.usrnumerodocumento).FirstOrDefault();

                    PdpPagpPago pago = new PdpPagpPago();

                    pago.UsridUsuario = usuario.UsridUsuario;
                    pago.OpeidOperacion = Convert.ToInt64(pagoActualizar.opeidOperacion);
                    pago.PagcodigoCliente = pagoActualizar.pagcodigoCliente;
                    pago.PagcodigoComprobante = pagoActualizar.pagcodigoComprobante;
                    pago.Opemonto = Convert.ToDecimal(pagoActualizar.opemonto);
                    pago.Opesuministro = Convert.ToInt64(pagoActualizar.opesuministro);
                    pago.PagmetodoPago = 2;
                    pago.Pagcreado = DateTime.Now;
                    pago.Pagbrand = pagoActualizar.pagbrand;
                    pago.Pagcard = pagoActualizar.pagcard;
                    pago.Pagdescription = pagoActualizar.pagdescription;
                    pago.Pagestado = pagoActualizar.pagestado;
                    pago.PAGRespuestaSielse = clsPagoDeudaRespuesta.MensajeRetornoPago;

                    _context.Add(pago);
                    _context.SaveChangesAsync();

                    response = Ok(new { Estado = "OK" });

                    try
                    {
                        var path = _env.WebRootPath + "/plantilla_email/confirmacion_pago.html";
                        String fileContents = System.IO.File.ReadAllText(path);

                        fileContents = fileContents.Replace("%%PurchaseNumber%%", pagoActualizar.opeidOperacion);
                        fileContents = fileContents.Replace("%%CodigoCliente%%", pagoActualizar.pagcodigoCliente);
                        fileContents = fileContents.Replace("%%NombreClienteDireccionCliente%%", pagoActualizar.nombrecliente + ", " + pagoActualizar.direccioncliente);
                        fileContents = fileContents.Replace("%%FechaEmision%%", pagoActualizar.fechaemision);
                        fileContents = fileContents.Replace("%%FechaVencimiento%%", pagoActualizar.fechavencimiento);
                        fileContents = fileContents.Replace("%%DetalleConsulta%%", pagoActualizar.detalleconsulta);
                        fileContents = fileContents.Replace("%%Total%%", pagoActualizar.opemonto);
                        fileContents = fileContents.Replace("%%Pagcreado%%", DateTime.Now.ToString());
                        fileContents = fileContents.Replace("%%Pagbrand%%", pagoActualizar.pagbrand);
                        fileContents = fileContents.Replace("%%Pagcard%%", pagoActualizar.pagcard);
                        fileContents = fileContents.Replace("%%Pagdescription%%", pagoActualizar.pagdescription);

                        SendEmailOutlook email_obj = new SendEmailOutlook(usuario.UsrcorreoPrimario, "Confirmación de pago", fileContents, _env.WebRootPath);
                        email_obj.Send();

                    }
                    catch (Exception ex)
                    {
                        response = Ok(new
                        {
                            Estado = "Error",
                            Mensaje = ex.Message,
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

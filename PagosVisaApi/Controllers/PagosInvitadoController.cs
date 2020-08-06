using Electrosur.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PagosVisaApi.Helper;
using PagosVisaApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using WSSielse;


namespace Electrosur.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PagosInvitadoController : ControllerBase
    {
        private IPrincipal _principal;
        private readonly ElectrosurContext _context;
        private readonly IConfiguration _configuration;

        public PagosInvitadoController(ElectrosurContext context, IPrincipal principal, IConfiguration configuration)
        {
            _context = context;
            _principal = principal;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Index([FromBody] FormPagosInvitado pago)
        {
            IActionResult response = Unauthorized();

            PdpUsrtUsuarioDelSistema usuario;
            try
            {
                usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrnumeroDocumento == pago.dni).FirstOrDefault();

                if (usuario == null)
                {
                    usuario = new PdpUsrtUsuarioDelSistema();
                    usuario.UsruniqueId = Guid.NewGuid();
                    usuario.UsrtipoDocumento = "DNI";
                    usuario.UsrnumeroDocumento = pago.dni;
                    usuario.Usrnombre = "Invitado";
                    usuario.UsrapellidoPaterno = "";
                    usuario.UsrapellidoMaterno = "";
                    usuario.UsrcorreoPrimario = "Invitado";
                    usuario.UsrcorreoSecundario = "";
                    usuario.Usrtelefono = "";
                    usuario.Usrcontrasena = "";
                    usuario.Usrestado = false;
                    usuario.Usrcreado = DateTime.Now;
                    usuario.Usrmodificado = DateTime.Now;
                    usuario.UsrultimoAcceso = DateTime.Now;
                    usuario.UsrconfirmacionCorreo = false;
                    usuario.UsrrecuperarContrasena = false;

                    _context.Add(usuario);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response = Ok(new
                {
                    Estado = "Error",
                    Mensaje = ex.Message,
                });

                return response;
            }


            List<Recibos> recibos = new List<Recibos>();


            try
            {
                SCobranzaClient sCobranzaClient = new SCobranzaClient();
                ClsConsultaDeudaPeticion clsConsultaDeudaPeticion = new ClsConsultaDeudaPeticion();
                String IdentificadorEntidadConsulta = Guid.NewGuid().ToString();
                clsConsultaDeudaPeticion.IdentificadorEntidadConsulta = IdentificadorEntidadConsulta;
                clsConsultaDeudaPeticion.AgenciaEntidadConsulta = _configuration["WebServices:AgenciaEntidadConsulta"];
                clsConsultaDeudaPeticion.MedioPagoEntidadConsulta = _configuration["WebServices:MedioPagoEntidadConsulta"];
                clsConsultaDeudaPeticion.FechaConsulta = DateTime.Now;
                clsConsultaDeudaPeticion.UsuarioConsulta = _configuration["WebServices:UsuarioConsulta"];
                clsConsultaDeudaPeticion.ClaveUsuario = _configuration["WebServices:ClaveUsuario"];
                clsConsultaDeudaPeticion.TipoConsulta = 1;
                clsConsultaDeudaPeticion.DatoConsulta = pago.codigocliente;
                var clsConsultaDeudaRespuesta = sCobranzaClient.ConsultaDeuda(clsConsultaDeudaPeticion);
                if (clsConsultaDeudaRespuesta.CodigoMensajeRetornoConsulta != 0)
                {
                    response = Ok(new
                    {
                        Estado = "Error",
                        Mensaje = clsConsultaDeudaRespuesta.MensajeRetornoConsulta,
                    });
                }
                else
                {
                    PdpOpepOperacion operacion = new PdpOpepOperacion();
                    operacion.Opecreado = DateTime.Now;
                    operacion.UsridUsuario = usuario.UsridUsuario;
                    operacion.Opesuministro = Convert.ToInt64(pago.codigocliente);
                    operacion.Opemonto = Convert.ToDecimal(clsConsultaDeudaRespuesta.MontoAPagarConsulta);
                    _context.Add(operacion);
                    _context.SaveChanges();

                    Cryptography crypto = new Cryptography();

                    if (clsConsultaDeudaRespuesta.DetalleConsulta.Equals("Recibo de Energía") ||
                        clsConsultaDeudaRespuesta.DetalleConsulta.Equals("Recibo de Energía que incluye 2 meses de deuda"))
                    {
                        recibos.Add(new Recibos()
                        {
                            CodigoComprobante = clsConsultaDeudaRespuesta.CodigoComprobante,
                            CodigoSuministro = clsConsultaDeudaRespuesta.CodigoSuministro,
                            NombreCliente = clsConsultaDeudaRespuesta.NombreCliente,
                            DireccionCliente = clsConsultaDeudaRespuesta.DireccionCliente,
                            FechaEmision = clsConsultaDeudaRespuesta.FechaEmision.ToString("dd/MM/yyyy"),
                            FechaVencimiento = clsConsultaDeudaRespuesta.FechaVencimiento.ToString("dd/MM/yyyy"),
                            DetalleConsulta = clsConsultaDeudaRespuesta.DetalleConsulta,
                            MontoAPagarConsulta = clsConsultaDeudaRespuesta.MontoAPagarConsulta.ToString("#.00"),
                            IdentificadorEntidadConsulta = crypto.Encrypt(clsConsultaDeudaRespuesta.IdentificadorTransaccion, _configuration["ClaveSecreta"]),
                            PurcharseNumber = operacion.OpeidOperacion.ToString()
                        });
                    }
                    else
                    {
                        recibos.Add(new Recibos()
                        {
                            CodigoComprobante = clsConsultaDeudaRespuesta.CodigoComprobante,
                            CodigoSuministro = clsConsultaDeudaRespuesta.CodigoSuministro,
                            NombreCliente = clsConsultaDeudaRespuesta.NombreCliente,
                            DireccionCliente = clsConsultaDeudaRespuesta.DireccionCliente,
                            FechaEmision = clsConsultaDeudaRespuesta.FechaEmision.ToString("dd/MM/yyyy"),
                            FechaVencimiento = "",
                            DetalleConsulta = clsConsultaDeudaRespuesta.DetalleConsulta,
                            MontoAPagarConsulta = clsConsultaDeudaRespuesta.MontoAPagarConsulta.ToString("#.00"),
                            IdentificadorEntidadConsulta = crypto.Encrypt(clsConsultaDeudaRespuesta.IdentificadorTransaccion, _configuration["ClaveSecreta"]),
                            PurcharseNumber = operacion.OpeidOperacion.ToString()
                        });
                    }
                    response = Ok(new { Estado = "OK", recibos = recibos });
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

using System;
using System.Collections.Generic;
using System.Linq;
using PagosVisaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WSSielse;
using Microsoft.Extensions.Configuration;

namespace PagosVisaWeb.Controllers
{

    public class Recibos
    {
        public String CodigoComprobante { get; set; }
        public String CodigoSuministro { get; set; }
        public String NombreCliente { get; set; }
        public String FechaEmision { get; set; }
        public String FechaVencimiento { get; set; }
        public String DetalleConsulta { get; set; }
        public String MontoAPagarConsulta { get; set; }
        public String DireccionCliente { get; set; }
        public String IdentificadorEntidadConsulta { get; set; }

    }

    /*******************************************************************************************
     * PagosController
     * Buscamos un numero de contrato y el historial de pagos
     * Programador: Rodercik Cusirramos Montesinos
     * Fecha de creacion: 22/06/2020
     * Fecha de modificacion: 03/08/2020      
     * *****************************************************************************************/

    public class PagosController : Controller
    {
        private readonly ElectrosurContext _context;
        private readonly IConfiguration _configuration;

        public PagosController(ElectrosurContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("/BuscarSuministro")]
        
        public ActionResult BuscarSuministro() => View();

        /// <summary>
        /// Buscamos un suministro 
        /// Lee los datos del web services
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/BuscarSuministro")]
        public ActionResult BuscarSuministro(String codigo_cliente)
        {

            ViewBag.codigo_cliente = codigo_cliente;
            String IdentificadorEntidadConsulta = Guid.NewGuid().ToString();
            List<Recibos> recibos = new List<Recibos>();

            
            try
            {               
               
                SCobranzaClient sCobranzaClient = new SCobranzaClient();
                ClsConsultaDeudaPeticion clsConsultaDeudaPeticion = new ClsConsultaDeudaPeticion();                

                clsConsultaDeudaPeticion.IdentificadorEntidadConsulta = IdentificadorEntidadConsulta;
                clsConsultaDeudaPeticion.AgenciaEntidadConsulta = _configuration["WebServices:AgenciaEntidadConsulta"];
                clsConsultaDeudaPeticion.MedioPagoEntidadConsulta = _configuration["WebServices:MedioPagoEntidadConsulta"];
                clsConsultaDeudaPeticion.FechaConsulta = DateTime.Now;
                clsConsultaDeudaPeticion.UsuarioConsulta = _configuration["WebServices:UsuarioConsulta"];
                clsConsultaDeudaPeticion.ClaveUsuario = _configuration["WebServices:ClaveUsuario"];
                clsConsultaDeudaPeticion.TipoConsulta = 1;
                clsConsultaDeudaPeticion.DatoConsulta = codigo_cliente;

                var clsConsultaDeudaRespuesta = sCobranzaClient.ConsultaDeuda(clsConsultaDeudaPeticion);

                if (clsConsultaDeudaRespuesta.CodigoMensajeRetornoConsulta != 0)
                {
                    ViewBag.Error = clsConsultaDeudaRespuesta.MensajeRetornoConsulta;
                }
                else
                {
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
                            IdentificadorEntidadConsulta = clsConsultaDeudaRespuesta.IdentificadorTransaccion
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
                            IdentificadorEntidadConsulta = clsConsultaDeudaRespuesta.IdentificadorTransaccion
                        });
                    }
                   

                    if (recibos.Count > 0)
                    {
                        var first = recibos.OrderBy(m => m.FechaVencimiento).ToList().First();
                        HttpContext.Session.SetString("codigo_cliente", codigo_cliente);
                        HttpContext.Session.SetString("Total", first.MontoAPagarConsulta);
                        HttpContext.Session.SetString("CodigoComprobante", first.CodigoComprobante);
                        HttpContext.Session.SetString("CodigoSuministro", first.CodigoSuministro);
                        HttpContext.Session.SetString("IdentificadorEntidadConsulta", first.IdentificadorEntidadConsulta);
                        var strRecibo = JsonConvert.SerializeObject(first);
                        HttpContext.Session.SetString("Recibo", strRecibo);
                    }
                   
                    ViewBag.Recibos = recibos.OrderBy(m => m.FechaVencimiento).ToList();                    
                }                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [Route("/MisPagos")]
        public ActionResult MisPagos() => View();

        /// <summary>
        /// Buscamos los pagos realizamos por año y mes 
        /// </summary>
        [Route("/MisPagos")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MisPagos(int Anyo, int Mes)
        {
            var USRidUsuario = Convert.ToInt64(HttpContext.Session.GetString("USRidUsuario"));
            var list = _context.PdpPagpPago.Where(m => m.UsridUsuario == USRidUsuario && 
            m.Pagcreado.Year == Anyo && 
            m.Pagcreado.Month == Mes).OrderByDescending(m => m.Pagcreado);
            return View(list);
        }
    }
}

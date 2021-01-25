using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PagosVisaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WSSielse;
using Microsoft.AspNetCore.Hosting;
using PagosVisaWeb.Helper;

namespace PagosVisaWeb.Controllers
{
    /*******************************************************************************************
 * VisaController
 * Este controlador lo usamos para realizar el pago en Visa.
 * Programador: Rodercik Cusirramos Montesinos
 * Fecha de creacion: 22/06/2020
 * Fecha de modificacion: 03/08/2020      
 * *****************************************************************************************/
    public class VisaController : Controller
    {
        private readonly ElectrosurContext _context;
        private readonly IConfiguration _configuration;
        String Error_Token_Seguridad = "";
        String Error_Token_Session = "";
        private readonly IWebHostEnvironment _env;

        public VisaController(ElectrosurContext context, IConfiguration configuration, IWebHostEnvironment env)
        {
            _context = context;
            _configuration = configuration;
            _env = env;
        }

        /// <summary>
        /// Carga el Resumen de pago y en la vista esta la interfaz por JavaScript con Visa
        /// </summary>
        /// <returns></returns>
        [Route("/PagarVisa")]
        public async Task<IActionResult> Pago()
        {
            try
            {
                ViewBag.codigo_cliente = HttpContext.Session.GetString("codigo_cliente");
                String strRecibo = HttpContext.Session.GetString("Recibo");
                ViewBag.Recibo = Newtonsoft.Json.JsonConvert.DeserializeObject<Recibos>(strRecibo);

                HttpContext.Session.GetString("CodigoComprobante");


                var amount = Convert.ToDecimal(HttpContext.Session.GetString("Total"));
                String amount_str = amount.ToString("0.00");
                ViewBag.Total = amount_str;

                String Token_Seguridad = token_seguridad();
                String Token_Session = session_comunicacion(amount_str, Token_Seguridad);

                ViewBag.Token_Seguridad = Token_Seguridad;
                ViewBag.Token_Session = Token_Session;
                ViewBag.Error_Token_Session = Error_Token_Session;



                if (_configuration["Datos:Estado"].ToString().Equals("Desarrollo"))                
                    ViewBag.MerchantID = _configuration["VisaDesarrollo:MerchantId"];
                
                if (_configuration["Datos:Estado"].ToString().Equals("Produccion"))                
                    ViewBag.MerchantID = _configuration["VisaProduccion:MerchantId"];
                

                PdpOpepOperacion operacion = new PdpOpepOperacion();
                operacion.Opecreado = DateTime.Now;
                operacion.UsridUsuario = Convert.ToInt64(HttpContext.Session.GetString("USRidUsuario"));
                operacion.Opesuministro = 1;
                operacion.Opemonto = amount;

                _context.Add(operacion);
                await _context.SaveChangesAsync();

                ViewBag.PurchaseNumber = operacion.OpeidOperacion;

                HttpContext.Session.SetString("IdOperacion", operacion.OpeidOperacion.ToString());
                HttpContext.Session.SetString("Monto", amount_str);
                HttpContext.Session.SetString("Token_Seguridad", Token_Seguridad);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [Route("/ConfirmacionPago")]
        public async Task<IActionResult> ConfirmacionPago(String transactionToken)
        {
            String VisaUrlAuthorization = "";

            var tmp = _configuration["Datos:Estado"];

            if (_configuration["Datos:Estado"].ToString().Equals("Desarrollo"))            
                VisaUrlAuthorization = _configuration["VisaDesarrollo:VisaUrlAuthorization"];
            
            if (_configuration["Datos:Estado"].ToString().Equals("Produccion"))            
                VisaUrlAuthorization = _configuration["VisaProduccion:VisaUrlAuthorization"];
            

            List<String> error = new List<string>();
            error.Add(transactionToken);
            RootObject respuesta_visa = new RootObject();
            RootObjectError respuesta_visa_error = new RootObjectError();
            bool flag = false;

            String Amount = HttpContext.Session.GetString("Monto");
            String PurchaseNumber = HttpContext.Session.GetString("IdOperacion");
            String tokenSeguridad = HttpContext.Session.GetString("Token_Seguridad");

            String body = "{" +
               "\"antifraud\":null," +
               "\"captureType\":\"manual\"," +
               "\"channel\":\"web\"," +
               "\"countable\":false," +
               "\"order\":{" +
                           "\"amount\":\"" + Amount + "\"," +
                           "\"tokenId\":\"" + transactionToken + "\"," +
                           "\"purchaseNumber\":\"" + PurchaseNumber + "\"," +
                           "\"currency\":\"PEN\"" +
                          "}" +
               "}";

            error.Add(body);
            String respuesta = "";

            try
            {
                HttpWebRequest request = WebRequest.Create(VisaUrlAuthorization) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                using (var rs = request.GetRequestStream())
                {
                    using (StreamWriter sw = new StreamWriter(rs))
                    {
                        sw.Write(body);
                    }
                }

                request.Headers["Authorization"] = tokenSeguridad;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        String json = sr.ReadToEnd().ToString();
                        error.Add(json);
                        respuesta_visa = System.Text.Json.JsonSerializer.Deserialize<RootObject>(json);
                        flag = true;

                        try
                        {

                            SCobranzaClient sCobranzaClient = new SCobranzaClient();
                            ClsPagoDeudaPeticion clsPagoDeudaPeticion = new ClsPagoDeudaPeticion();
                            clsPagoDeudaPeticion.IdentificadorTransaccion = HttpContext.Session.GetString("IdentificadorEntidadConsulta");
                            clsPagoDeudaPeticion.IdentificadorEntidadPago = Guid.NewGuid().ToString();
                            clsPagoDeudaPeticion.AgenciaEntidadPago = _configuration["WebServices:AgenciaEntidadConsulta"];
                            clsPagoDeudaPeticion.MedioPagoEntidadPago = _configuration["WebServices:MedioPagoEntidadConsulta"];
                            clsPagoDeudaPeticion.FechaPago = DateTime.Now;
                            clsPagoDeudaPeticion.UsuarioPago = _configuration["WebServices:UsuarioConsulta"];
                            clsPagoDeudaPeticion.ClaveUsuario = _configuration["WebServices:ClaveUsuario"];
                            clsPagoDeudaPeticion.CodigoComprobante = HttpContext.Session.GetString("CodigoComprobante");
                            clsPagoDeudaPeticion.CodigoSuministro = HttpContext.Session.GetString("CodigoSuministro");
                            clsPagoDeudaPeticion.MontoPago = Convert.ToDecimal(HttpContext.Session.GetString("Monto"));
                            var clsPagoDeudaRespuesta = sCobranzaClient.PagoDeuda(clsPagoDeudaPeticion);

                            try
                            {
                                PdpPagpPago pago = new PdpPagpPago();
                                pago.UsridUsuario = Convert.ToInt64(HttpContext.Session.GetString("USRidUsuario"));
                                pago.OpeidOperacion = Convert.ToInt64(HttpContext.Session.GetString("IdOperacion"));
                                pago.PagcodigoCliente = HttpContext.Session.GetString("codigo_cliente");
                                pago.PagcodigoComprobante = HttpContext.Session.GetString("CodigoComprobante");
                                pago.Opemonto = Convert.ToDecimal(HttpContext.Session.GetString("Monto"));
                                pago.Opesuministro = Convert.ToInt64(HttpContext.Session.GetString("CodigoSuministro"));
                                pago.PagmetodoPago = 1;
                                pago.Pagcreado = DateTime.Now;
                                pago.Pagbrand = respuesta_visa.dataMap.BRAND;
                                pago.Pagcard = respuesta_visa.dataMap.CARD;
                                pago.Pagdescription = respuesta_visa.dataMap.ACTION_DESCRIPTION;
                                pago.Pagestado = "CORRECTO";
                                pago.PAGRespuestaSielse = clsPagoDeudaRespuesta.MensajeRetornoPago;
                                _context.Add(pago);
                                await _context.SaveChangesAsync();
                                ViewBag.Pago = pago;
                                ViewBag.Estado = "OK";

                            }
                            catch (Exception ex)
                            {
                                ViewBag.Error = "Error en actualizar el pago en base de datos historial, " + ex.Message;
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = "Error en actualizar el pago en el sistema comercial, " + ex.Message;
                        }

                        try
                        {

                            String UsrcorreoPrimario = HttpContext.Session.GetString("USRCorreoPrimario");
                            var path = _env.WebRootPath + "/plantilla_email/confirmacion_pago.html";
                            String fileContents = System.IO.File.ReadAllText(path);


                            String strRecibo = HttpContext.Session.GetString("Recibo");
                            var Recibo = Newtonsoft.Json.JsonConvert.DeserializeObject<Recibos>(strRecibo);

                            fileContents = fileContents.Replace("%%PurchaseNumber%%", HttpContext.Session.GetString("IdOperacion"));
                            fileContents = fileContents.Replace("%%CodigoCliente%%", HttpContext.Session.GetString("codigo_cliente"));
                            fileContents = fileContents.Replace("%%CodigoSuministro%%", HttpContext.Session.GetString("CodigoSuministro"));
                            fileContents = fileContents.Replace("%%NombreClienteDireccionCliente%%", Recibo.NombreCliente + ", " + Recibo.DireccionCliente);
                            fileContents = fileContents.Replace("%%FechaEmision%%", Recibo.FechaEmision);
                            fileContents = fileContents.Replace("%%FechaVencimiento%%", Recibo.FechaVencimiento);
                            fileContents = fileContents.Replace("%%DetalleConsulta%%", Recibo.DetalleConsulta);
                            fileContents = fileContents.Replace("%%Total%%", HttpContext.Session.GetString("Monto"));
                            fileContents = fileContents.Replace("%%Pagcreado%%", DateTime.Now.ToString());
                            fileContents = fileContents.Replace("%%Pagbrand%%", respuesta_visa.dataMap.BRAND);
                            fileContents = fileContents.Replace("%%Pagcard%%", respuesta_visa.dataMap.CARD);
                            fileContents = fileContents.Replace("%%Pagdescription%%", respuesta_visa.dataMap.ACTION_DESCRIPTION);

                            SendEmailOutlook email_obj = new SendEmailOutlook(UsrcorreoPrimario, "Confirmación de pago", fileContents, _env.WebRootPath);
                            email_obj.Send();

                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = "Error de envio de correo, " + ex.Message;
                        }
                    }
                }
            }

            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    var httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(data);
                        respuesta = sr.ReadToEnd();
                        error.Add(respuesta);
                        respuesta_visa_error = JsonSerializer.Deserialize<RootObjectError>(respuesta);

                        PdpPagpPago pago = new PdpPagpPago();

                        pago.UsridUsuario = Convert.ToInt64(HttpContext.Session.GetString("USRidUsuario"));
                        pago.OpeidOperacion = Convert.ToInt64(HttpContext.Session.GetString("IdOperacion"));

                        pago.PagcodigoCliente = HttpContext.Session.GetString("codigo_cliente");
                        pago.PagcodigoComprobante = HttpContext.Session.GetString("CodigoComprobante");
                        pago.Opemonto = Convert.ToDecimal(HttpContext.Session.GetString("Monto"));
                        pago.Opesuministro = Convert.ToInt64(HttpContext.Session.GetString("CodigoSuministro"));
                        pago.PagmetodoPago = 1;
                        pago.Pagcreado = DateTime.Now;
                        pago.Pagbrand = respuesta_visa_error.data.BRAND;
                        pago.Pagcard = respuesta_visa_error.data.CARD;
                        pago.Pagdescription = respuesta_visa_error.data.ACTION_DESCRIPTION;
                        pago.Pagestado = "Error";

                        _context.Add(pago);
                        await _context.SaveChangesAsync();

                        ViewBag.Pago = pago;
                        ViewBag.Estado = "Error";

                    }
                }
            }
            return View();
        }

        public String token_seguridad()
        {
            String merchantid = "", AccessKeyId = "", SecretAccessKey = "", url_seguridad = "";

            var tmp = _configuration["Datos:Estado"];

            if (_configuration["Datos:Estado"].ToString().Equals("Desarrollo"))
            {
                merchantid = _configuration["VisaDesarrollo:MerchantId"];
                AccessKeyId = _configuration["VisaDesarrollo:AccessKeyId"];
                SecretAccessKey = _configuration["VisaDesarrollo:SecretAccessKey"];
                url_seguridad = _configuration["VisaDesarrollo:VisaUrlSeguridad"];
            }
            if (_configuration["Datos:Estado"].ToString().Equals("Produccion"))
            {
                merchantid = _configuration["VisaProduccion:MerchantId"];
                AccessKeyId = _configuration["VisaProduccion:AccessKeyId"];
                SecretAccessKey = _configuration["VisaProduccion:SecretAccessKey"];
                url_seguridad = _configuration["VisaProduccion:VisaUrlSeguridad"];
            }

            String tokenSeguridad = "";
            try
            {
                HttpWebRequest request = WebRequest.Create(url_seguridad) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "text/plain";
                request.Accept = "text/plain";
                string credidentials = AccessKeyId + ":" + SecretAccessKey;
                String authorization = Convert.ToBase64String(Encoding.Default.GetBytes(credidentials));
                request.Headers["Authorization"] = "Basic " + authorization;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        tokenSeguridad = sr.ReadToEnd().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error_Token_Seguridad = ex.Message;
            }
            return tokenSeguridad;
        }


        public String session_comunicacion(String amount, String tokenSeguridad)
        {
            String url_session = "";

            if (_configuration["Datos:Estado"].ToString().Equals("Desarrollo"))
            {
                url_session = _configuration["VisaDesarrollo:VisaUrlSession"];
            }
            if (_configuration["Datos:Estado"].ToString().Equals("Produccion"))
            {
                url_session = _configuration["VisaProduccion:VisaUrlSession"];
            }

            String clientIp = _configuration["Datos:IpServidor"].ToString();

            String TokenSession = "";
            string body =
                 "{" +
                   "\"amount\": \"" + amount + "\"," +
                   "\"antifraud\":{" +
                       "\"clientIp\":\"" + clientIp + "\"," +
                       "\"merchantDefineData\":{" +
                            "\"MDD4\":\"" + HttpContext.Session.GetString("USRCorreoPrimario") + "\"," +
                            "\"MDD21\":\"" + "0" + "\"," +
                            "\"MDD32\":\"" + HttpContext.Session.GetString("UsrtipoDocumento") + "\"," +
                            "\"MDD75\":\"" + HttpContext.Session.GetString("UsrnumeroDocumento") + "\"," +
                            "\"MDD77\":\"" + HttpContext.Session.GetString("UsrCreadoDias") + "\"" +
                        "}" +
                    "}," +
                    "\"channel\":\"web\"," +
                    "\"recurrenceMaxAmount\":\"" + amount + "\"" +
            "}";

            try
            {
                HttpWebRequest request = WebRequest.Create(url_session) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                using (var rs = request.GetRequestStream())
                {
                    using (StreamWriter sw = new StreamWriter(rs))
                    {
                        sw.Write(body);
                    }
                }

                request.Headers["Authorization"] = tokenSeguridad;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        TokenSession = sr.ReadToEnd().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Error_Token_Session = ex.Message;
                return null;
            }

            ClsTokenSession routes_list = JsonSerializer.Deserialize<ClsTokenSession>(TokenSession);
            return routes_list.sessionKey;
        }

    }
}

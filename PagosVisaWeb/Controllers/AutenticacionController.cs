using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PagosVisaWeb.Helper;
using PagosVisaWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PagosVisaWeb.Controllers
{
    /*******************************************************************************************
     * PagosVisaWeb
     * Este controlador lo usamos para todo lo es autenticación.
     * Programador: Rodercik Cusirramos Montesinos
     * Fecha de creacion: 22/06/2020
     * Fecha de modificacion: 03/08/2020      
     * *****************************************************************************************/

    public class AutenticacionController : Controller
    {
        private readonly ElectrosurContext _context;
        private readonly IWebHostEnvironment _env;

        public AutenticacionController(ElectrosurContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        /// <summary>
        /// Vista Logout
        /// Limpiamos a sesión y redireccionamos al inicio
        /// </summary>
        [Route("/Salir")]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("USRidUsuario", "");
            HttpContext.Session.SetString("USRNombre", "");
            HttpContext.Session.SetString("UsruniqueId", "");
            return Redirect("/");
        }

        [Route("/")]
        public IActionResult Login() => View();

        /// <summary>
        /// Vista Login 
        /// Esta accion ejecuta el metodo post del Login, recibe 3 parametros
        /// email, password, CaptchaCode, hacemos las siguintes validadiones:
        /// - Ingreso de email, password, CaptchaCode
        /// - CaptchaCode valiamos con la sesion almacenada cunado se genero la imagen
        /// - Validamos email con UsrcorreoPrimario y password con Usrcontrasena con la base de datos
        /// - Si lo encotramos en la base de datos y su correo esta validado procedemos alamcenas las variables session
        /// </summary>
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String email, String password, String captchaCode)
        {

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
                ViewBag.Message = "Ingrese email o contraseña";
                return View();
            }

            try
            {
                password = PasswordHash.GetMd5Hash(password);

                if (captchaCode == HttpContext.Session.GetString("CaptchaCode"))
                {
                    var usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrcorreoPrimario == email &&
                    m.Usrcontrasena == password).FirstOrDefault();

                    if (usuario != null)
                    {
                        if (usuario.UsrconfirmacionCorreo)
                        {
                            HttpContext.Session.SetString("USRidUsuario", usuario.UsridUsuario.ToString());
                            HttpContext.Session.SetString("USRCorreoPrimario", usuario.UsrcorreoPrimario);
                            HttpContext.Session.SetString("USRNombre", usuario.Usrnombre +" " + usuario.UsrapellidoPaterno + " " + usuario.UsrapellidoMaterno);
                            HttpContext.Session.SetString("UsruniqueId", usuario.UsruniqueId.ToString());
                            return Redirect("/BuscarSuministro");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Correo no confirmado");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Email o contraseña invalidos");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error en captcha");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Email o contraseña invalidos");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        /// <summary>
        /// Funcion devuelve una imagen png
        /// Creamos un código captcha y lo almacenamos en una sesion CaptchaCode, 
        /// con este código generamos una imagen.
        /// </summary>
        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }

        /// <summary>
        /// Confirma el correo de un usuario
        /// Cuando la persona se registra se le envia un correo que tiene que confirmar,
        /// en ese correo hay un link el cual direcciona a esta acción.
        /// Actualiza en base de datos UsrconfirmacionCorreo a true
        /// </summary>
        [Route("confirmar")]
        public IActionResult ConfrimacioDeCorreo(Guid uniqueid)
        {
            try
            {
                var usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsruniqueId == uniqueid).FirstOrDefault();
                if (usuario != null)
                {
                    usuario.UsrconfirmacionCorreo = true;
                    _context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Codigo invalido");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Codigo invalido");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [Route("RecuperarContrasena")]
        public IActionResult RecuperarContrasena() => View();

        /// <summary>
        /// Para recuperar contraseña ingresamos el emaily captchaCode
        /// Usamos el generador del captcha para crear una clave temporal y
        /// se la enviamos por correo
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("RecuperarContrasena")]
        public IActionResult RecuperarContrasena(String email, String captchaCode)
        {
            try
            {
                if (captchaCode == HttpContext.Session.GetString("CaptchaCode"))
                {

                    var usuario = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrcorreoPrimario == email).FirstOrDefault();

                    if (usuario != null)
                    {
                        var captchaCodeGenerado = Captcha.GenerateCaptchaCode();
                        usuario.Usrcontrasena = PasswordHash.GetMd5Hash(captchaCodeGenerado);
                        usuario.Usrmodificado = DateTime.Now;
                        usuario.UsrultimoAcceso = DateTime.Now;
                        _context.Update(usuario);
                        _context.SaveChanges();

                        

                        var path = _env.WebRootPath + "/plantilla_email/recuperarcontrasena_email.html";
                        String fileContents = System.IO.File.ReadAllText(path);
                        fileContents = fileContents.Replace("$$NuevaContrasena$$", captchaCodeGenerado);

                        SendEmailOutlook email_obj = new SendEmailOutlook(usuario.UsrcorreoPrimario, "Recuperar contraseña", fileContents, _env.WebRootPath);
                        email_obj.Send();

                        if (!email_obj.Error.Equals(""))
                        {
                            ModelState.AddModelError(string.Empty, email_obj.Error);
                            return View();
                        }

                        ViewData["Grabado"] = true;
                        return View();
                    }

                    ModelState.AddModelError(string.Empty, "Error correo no existe");
                    return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error en captcha");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [Route("ActualizarContrasena")]
        public IActionResult ActualizarContrasena() => View();

        /// <summary>
        /// Actualizamos la contraseña del usuario actual.
        /// </summary>
        [Route("ActualizarContrasena")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActualizarContrasena(String password)
        {
            long id = Convert.ToInt64(HttpContext.Session.GetString("USRidUsuario"));

            var user = _context.PdpUsrtUsuarioDelSistema.Find(id);
            user.Usrcontrasena = PasswordHash.GetMd5Hash(password);
            user.Usrmodificado = DateTime.Now;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("BuscarSuministro", "Pagos");
        }


        [Route("Registro")]
        public IActionResult Registro() => View();

        /// <summary>
        /// Registramos a un nuevo usuario.
        /// Al grabar en base de datos enviamos un correo para validar su correo.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Registro")]
        public async Task<IActionResult> Registro([Bind("UsrtipoDocumento,UsrnumeroDocumento,Usrnombre,UsrapellidoPaterno,UsrapellidoMaterno,UsrcorreoPrimario,UsrcorreoSecundario,Usrtelefono,Usrcontrasena")] PdpUsrtUsuarioDelSistema pdpUsrtUsuarioDelSistema, String CaptchaCode)
        {
            try {

                if (CaptchaCode == HttpContext.Session.GetString("CaptchaCode")) {

                    var usuariorepetido = _context.PdpUsrtUsuarioDelSistema.Where(m => m.UsrnumeroDocumento == pdpUsrtUsuarioDelSistema.UsrnumeroDocumento ||
                    m.UsrcorreoPrimario == pdpUsrtUsuarioDelSistema.UsrcorreoPrimario).FirstOrDefault();

                    if (usuariorepetido != null)
                    {
                        if (usuariorepetido.UsrnumeroDocumento == pdpUsrtUsuarioDelSistema.UsrnumeroDocumento)
                        {
                            ModelState.AddModelError(string.Empty, "Número de documento repetido");
                        }

                        if (usuariorepetido.UsrcorreoPrimario == pdpUsrtUsuarioDelSistema.UsrcorreoPrimario)
                        {
                            ModelState.AddModelError(string.Empty, "Correo primario repetido");
                        }

                        return View(pdpUsrtUsuarioDelSistema);
                    }

                    pdpUsrtUsuarioDelSistema.Usrcontrasena = PasswordHash.GetMd5Hash(pdpUsrtUsuarioDelSistema.Usrcontrasena);
                    pdpUsrtUsuarioDelSistema.UsruniqueId = Guid.NewGuid();
                    pdpUsrtUsuarioDelSistema.Usrcreado = DateTime.Now;
                    pdpUsrtUsuarioDelSistema.Usrmodificado = DateTime.Now;
                    pdpUsrtUsuarioDelSistema.UsrultimoAcceso = DateTime.Now;
                    pdpUsrtUsuarioDelSistema.Usrestado = true;
                    pdpUsrtUsuarioDelSistema.UsrconfirmacionCorreo = false;
                    pdpUsrtUsuarioDelSistema.UsrrecuperarContrasena = false;

                    if (String.IsNullOrEmpty(pdpUsrtUsuarioDelSistema.UsrcorreoSecundario))                   
                        pdpUsrtUsuarioDelSistema.UsrcorreoSecundario = "-";
                    

                    _context.Add(pdpUsrtUsuarioDelSistema);
                    await _context.SaveChangesAsync();
                    ViewData["Grabado"] = true;

                    var path = _env.WebRootPath + "/plantilla_email/confirmacion_email.html";
                    String fileContents = System.IO.File.ReadAllText(path);
                    fileContents = fileContents.Replace("$$UniqueId$$", pdpUsrtUsuarioDelSistema.UsruniqueId.ToString());

                    SendEmailOutlook email = new SendEmailOutlook(pdpUsrtUsuarioDelSistema.UsrcorreoPrimario, "Electrosur confirmacion de correo", fileContents, _env.WebRootPath);
                    email.Send();

                    return View(pdpUsrtUsuarioDelSistema);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error en captcha");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(pdpUsrtUsuarioDelSistema);
        }

        [Route("error/{errorCode}")]
        public IActionResult ErrorPage(int errorCode)
        {
            return View();
        }       
    }
}

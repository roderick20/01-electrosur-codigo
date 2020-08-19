using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electrosur.Models
{
    public class FormRegistro
    {
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string correoPrimario { get; set; }
        public string correoSecundario { get; set; }
        public string numeroTelefono { get; set; }
        public string contrasena { get; set; }
    }

    public class FormPagoActualizar
    {
        public string usridUsuario { get; set; }
        public string opeidOperacion { get; set; }
        public string pagcodigoCliente { get; set; }
        public string pagcodigoComprobante { get; set; }
        public string opemonto { get; set; }
        public string opesuministro { get; set; }
        public string pagbrand { get; set; }
        public string pagcard { get; set; }
        public string pagdescription { get; set; }
        public string pagestado { get; set; }
        public string detalleconsulta { get; set; }
        public string fechaemision { get; set; }
        public string fechavencimiento { get; set; }
        public string nombrecliente { get; set; }
        public string direccioncliente { get; set; }
        public string identificadorintidadionsulta { get; set; }
    }

    public class FormPagoActualizarInvitado
    {
        public string usrnumerodocumento { get; set; }
        public string opeidOperacion { get; set; }
        public string pagcodigoCliente { get; set; }
        public string pagcodigoComprobante { get; set; }
        public string opemonto { get; set; }
        public string opesuministro { get; set; }
        public string pagbrand { get; set; }
        public string pagcard { get; set; }
        public string pagdescription { get; set; }
        public string pagestado { get; set; }
        public string detalleconsulta { get; set; }
        public string fechaemision { get; set; }
        public string fechavencimiento { get; set; }
        public string nombrecliente { get; set; }
        public string direccioncliente { get; set; }
        public string identificadorintidadionsulta { get; set; }
    }

    public class FormRequest
    {
        public string d { get; set; }
    }

    public class FormLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class FormPagos
    {

        public String codigocliente { get; set; }
        public String usruniqueid { get; set; }
    }

    public class FormPagosInvitado
    {

        public String codigocliente { get; set; }
        public String dni { get; set; }
    }

    public class FormHistorial
    {
        public String usruniqueid { get; set; }
        public String anyo { get; set; }
        public String mes { get; set; }
    }

    public class FormRecuperarContrasena
    {
        public String correo { get; set; }
    }

    public class CambioContrasena
    {
        public String usruniqueid { get; set; }
        public String contrasena { get; set; }

    }


}

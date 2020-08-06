using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagosVisaApi.Models
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
        public String PurcharseNumber { get; set; }
        public String IdentificadorEntidadConsulta { get; set; }
    }
}

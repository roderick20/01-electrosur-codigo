using System;
using System.Collections.Generic;

namespace Electrosur.Models
{
    public partial class PdpPagpPago
    {
        public long PagidPago { get; set; }
        public long UsridUsuario { get; set; }
        public long OpeidOperacion { get; set; }
        public string PagcodigoCliente { get; set; }
        public string PagcodigoComprobante { get; set; }
        public decimal Opemonto { get; set; }
        public long Opesuministro { get; set; }
        public int PagmetodoPago { get; set; }
        public DateTime Pagcreado { get; set; }
        public string Pagbrand { get; set; }
        public string Pagcard { get; set; }
        public string Pagdescription { get; set; }
        public string Pagestado { get; set; }

        public string PAGRespuestaSielse { get; set; }

        public virtual PdpOpepOperacion OpeidOperacionNavigation { get; set; }
        public virtual PdpUsrtUsuarioDelSistema UsridUsuarioNavigation { get; set; }
    }
}

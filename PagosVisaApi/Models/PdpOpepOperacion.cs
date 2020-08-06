using System;
using System.Collections.Generic;

namespace Electrosur.Models
{
    public partial class PdpOpepOperacion
    {
        public PdpOpepOperacion()
        {
            PdpPagpPago = new HashSet<PdpPagpPago>();
        }

        public long OpeidOperacion { get; set; }
        public DateTime Opecreado { get; set; }
        public long UsridUsuario { get; set; }
        public long Opesuministro { get; set; }
        public decimal Opemonto { get; set; }

        public virtual PdpUsrtUsuarioDelSistema UsridUsuarioNavigation { get; set; }
        public virtual ICollection<PdpPagpPago> PdpPagpPago { get; set; }
    }
}

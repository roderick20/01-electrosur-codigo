using System;
using System.Collections.Generic;

namespace PagosVisaWeb.Models
{
    public partial class PdpUsrtUsuarioDelSistema
    {
        public PdpUsrtUsuarioDelSistema()
        {
            PdpOpepOperacion = new HashSet<PdpOpepOperacion>();
            PdpPagpPago = new HashSet<PdpPagpPago>();
        }

        public long UsridUsuario { get; set; }
        public Guid UsruniqueId { get; set; }
        public string UsrtipoDocumento { get; set; }
        public string UsrnumeroDocumento { get; set; }
        public string Usrnombre { get; set; }
        public string UsrapellidoPaterno { get; set; }
        public string UsrapellidoMaterno { get; set; }
        public string UsrcorreoPrimario { get; set; }
        public string UsrcorreoSecundario { get; set; }
        public string Usrtelefono { get; set; }
        public string Usrcontrasena { get; set; }
        public bool Usrestado { get; set; }
        public DateTime Usrcreado { get; set; }
        public DateTime Usrmodificado { get; set; }
        public DateTime UsrultimoAcceso { get; set; }
        public bool UsrconfirmacionCorreo { get; set; }
        public bool UsrrecuperarContrasena { get; set; }

        public virtual ICollection<PdpOpepOperacion> PdpOpepOperacion { get; set; }
        public virtual ICollection<PdpPagpPago> PdpPagpPago { get; set; }
    }
}

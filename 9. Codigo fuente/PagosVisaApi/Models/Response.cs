using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagosVisaApi.Models
{
    public class ResponseLogin
    {
        public string Token { get; set; }
        public string USRNombre { get; set; }
        public string UsruniqueId { get; set; }

        public string USRidUsuario { get; set; }
        public string USRCorreoPrimario { get; set; }
        public string UsrtipoDocumento { get; set; }
        public string UsrnumeroDocumento { get; set; }
    }
}

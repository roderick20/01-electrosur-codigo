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
    }
}

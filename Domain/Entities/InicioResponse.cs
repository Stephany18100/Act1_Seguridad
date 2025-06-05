using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InicioResponse
    {
        public string? UserName { get; set; }
        public string? AccesToken { get; set; }
        public int ExpirationToken { get; set; }
    }
}
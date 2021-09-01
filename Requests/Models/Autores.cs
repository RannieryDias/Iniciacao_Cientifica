using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Models
{
    public class Autores
    {
        public string uri { get; set; }
        public string nome { get; set; }
        public int codTipo { get; set; }
        public string tipo { get; set; }
        public int ordemAssinatura { get; set; }
        public int proponente { get; set; }
    }
}

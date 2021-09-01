using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models.Responses.Autores_Response
{
    public class Dado
    {
        public string uri { get; set; }
        public string nome { get; set; }
        public int codTipo { get; set; }
        public string tipo { get; set; }
        public int ordemAssinatura { get; set; }
        public int proponente { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class AutoresResponse
    {
        public List<Dado> dados { get; set; }
        public List<Link> links { get; set; }
    }

}

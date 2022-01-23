using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models.Responses.Legislatura
{
    public class Dado
    {
        public int id { get; set; }
        public string uri { get; set; }
        public string dataInicio { get; set; }
        public string dataFim { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class LegislaturaResponse
    {
        public List<Dado> dados { get; set; }
        public List<Link> links { get; set; }
    }


}

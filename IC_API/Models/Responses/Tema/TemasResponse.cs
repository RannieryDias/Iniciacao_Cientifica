using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models.Responses.TemasResponse
{
    public class Dados
    {
        public string cod { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class TemasResponse
    {
        public List<Dados> dados { get; set; }
        public List<Link> links { get; set; }
    }
}

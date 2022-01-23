using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models.Responses.ProjetoTema
{
    public class Dado
    {
        public int codTema { get; set; }
        public string tema { get; set; }
        public int relevancia { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class ProjetoTemaResponse
    {
        public List<Dado> dados { get; set; }
        public List<Link> links { get; set; }
    }
}

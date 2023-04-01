using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.DTO.Proposicao
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Dado
    {
        public int id { get; set; }
        public string uri { get; set; }
        public string siglaTipo { get; set; }
        public int codTipo { get; set; }
        public int numero { get; set; }
        public int ano { get; set; }
        public string ementa { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class Root
    {
        public List<Dado> dados { get; set; }
        public List<Link> links { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models.Responses.MesaResponse
{
    public class Dado
    {
        [Key]
        public int id { get; set; }
        public string uri { get; set; }
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string uriPartido { get; set; }
        public string siglaUf { get; set; }
        public int idLegislatura { get; set; }
        public string urlFoto { get; set; }
        public object email { get; set; }
        public string dataInicio { get; set; }
        public string dataFim { get; set; }
        public string titulo { get; set; }
        public string codTitulo { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class MesaResponse
    {
        public List<Dado> dados { get; set; }
        public List<Link> links { get; set; }
    }
}

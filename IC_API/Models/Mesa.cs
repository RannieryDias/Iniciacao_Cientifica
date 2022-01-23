using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models
{
    public class Mesa
    {
        [Key]
        public int id { get; set; }
        public string uri { get; set; }
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string uriPartido { get; set; }
        public string siglaUf { get; set; }
        [Key]
        public int idLegislatura { get; set; }
        public string urlFoto { get; set; }
        public string dataInicio { get; set; }
        public string dataFim { get; set; }
        public string titulo { get; set; }
        [Key]
        public string codTitulo { get; set; }
    }
}

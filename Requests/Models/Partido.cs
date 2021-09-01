using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Models
{
    public class Partido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public string uri { get; set; }
        public Status status { get; set; }
        public object numeroEleitoral { get; set; }
        public string urlLogo { get; set; }
        public object urlWebSite { get; set; }
        public object urlFacebook { get; set; }
    }

    public class Lider
    {
        public string uri { get; set; }
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string uriPartido { get; set; }
        public string uf { get; set; }
        public int idLegislatura { get; set; }
        public string urlFoto { get; set; }
    }

    public class Status
    {
        public string data { get; set; }
        public string idLegislatura { get; set; }
        public string situacao { get; set; }
        public string totalPosse { get; set; }
        public string totalMembros { get; set; }
        public string uriMembros { get; set; }
        public Lider lider { get; set; }
    }
}

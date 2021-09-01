using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Model
{
    public class Deputado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string uri { get; set; }
        public string NomeCivil { get; set; }
        [ForeignKey("UltimoStatusId")]
        public UltimoStatus UltimoStatus { get; set; }
        public string Cpf { get; set; }
        public string Sexo { get; set; }
        public object urlWebsite { get; set; }
        public List<object> redeSocial { get; set; }
        public string dataNascimento { get; set; }
        public object dataFalecimento { get; set; }
        public string ufNascimento { get; set; }
        public string municipioNascimento { get; set; }
        public string escolaridade { get; set; }
    }

    public class UltimoStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string uri { get; set; }
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string uriPartido { get; set; }
        public string siglaUf { get; set; }
        public int idLegislatura { get; set; }
        public string urlFoto { get; set; }
        public object email { get; set; }
        public string data { get; set; }
        public string nomeEleitoral { get; set; }
        //public Gabinete gabinete { get; set; }
        public string situacao { get; set; }
        public string condicaoEleitoral { get; set; }
        public object descricaoStatus { get; set; }
    }
}

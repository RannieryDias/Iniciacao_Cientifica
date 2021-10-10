using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IC_API.Model
{
    public class Deputado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string uri { get; set; }
        public string nomeCivil { get; set; }
        public UltimoStatus ultimoStatus { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public string urlWebsite { get; set; }
        public string dataNascimento { get; set; }
        public string dataFalecimento { get; set; }
        public string ufNascimento { get; set; }
        public string municipioNascimento { get; set; }
        public string escolaridade { get; set; }
    }

    public class UltimoStatus
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
        public string email { get; set; }
        public string data { get; set; }
        public string nomeEleitoral { get; set; }
        public string situacao { get; set; }
        public string condicaoEleitoral { get; set; }
        public string descricaoStatus { get; set; }
    }
}

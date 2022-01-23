using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models.Responses.Deputado
{
    public class Gabinete
    {
        public string nome { get; set; }
        public string predio { get; set; }
        public string sala { get; set; }
        public string andar { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
    }

    public class UltimoStatus
    {
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
        //public Gabinete gabinete { get; set; }
        public string situacao { get; set; }
        public string condicaoEleitoral { get; set; }
        public string descricaoStatus { get; set; }
    }

    public class Dados
    {
        public int id { get; set; }
        public string uri { get; set; }
        public string nomeCivil { get; set; }
        public UltimoStatus ultimoStatus { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public string urlWebsite { get; set; }
        //public string redeSocial { get; set; }
        public string dataNascimento { get; set; }
        public string dataFalecimento { get; set; }
        public string ufNascimento { get; set; }
        public string municipioNascimento { get; set; }
        public string escolaridade { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class DeputadoResponse
    {
        public Dados dados { get; set; }
        public List<Link> links { get; set; }
    }
}

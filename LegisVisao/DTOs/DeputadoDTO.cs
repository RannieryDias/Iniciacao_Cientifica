namespace LegisVisao.DTOs.DeputadoDTO
{
    public class DeputadoDTO
    {
        public int id { get; set; }
        public string uri { get; set; }
        public string nomeCivil { get; set; }
        public UltimoStatusDTO ultimoStatus { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public string urlWebsite { get; set; }
        public string dataNascimento { get; set; }
        public string dataFalecimento { get; set; }
        public string ufNascimento { get; set; }
        public string municipioNascimento { get; set; }
        public string escolaridade { get; set; }
    }

    public class UltimoStatusDTO
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
        public string situacao { get; set; }
        public string condicaoEleitoral { get; set; }
        public string descricaoStatus { get; set; }
    }

    public class Root
    {
        public List<DeputadoDTO> dados { get; set; }
        public List<Link> links { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }
}
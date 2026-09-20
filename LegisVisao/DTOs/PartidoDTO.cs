namespace LegisVisao.DTOs.PartidoDTO
{
    public class PartidoDTO
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public string uri { get; set; }
        public StatusDTO status { get; set; }
        public string numeroEleitoral { get; set; }
        public string urlLogo { get; set; }
        public string urlWebSite { get; set; }
        public string urlFacebook { get; set; }
    }

    public class StatusDTO
    {
        public int id { get; set; }
        public string data { get; set; }
        public string idLegislatura { get; set; }
        public string situacao { get; set; }
        public string totalPosse { get; set; }
        public string totalMembros { get; set; }
        public string uriMembros { get; set; }
        public LiderDTO lider { get; set; }
    }

    public class LiderDTO
    {
        public int id { get; set; }
        public string uri { get; set; }
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string uriPartido { get; set; }
        public string uf { get; set; }
        public int idLegislatura { get; set; }
        public string urlFoto { get; set; }
    }

    public class Root
    {
        public List<PartidoDTO> dados { get; set; }
        public List<Link> links { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }
}
namespace LegisVisao.Models
{
    public class Lider
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

    public class Status
    {
        public int id { get; set; }
        public string data { get; set; }
        public string idLegislatura { get; set; }
        public string situacao { get; set; }
        public string totalPosse { get; set; }
        public string totalMembros { get; set; }
        public string uriMembros { get; set; }
        public Lider lider { get; set; }
    }

    public class Partido
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public string uri { get; set; }
        public Status status { get; set; }
        public string numeroEleitoral { get; set; }
        public string urlLogo { get; set; }
        public string urlWebSite { get; set; }
        public string urlFacebook { get; set; }
    }

}

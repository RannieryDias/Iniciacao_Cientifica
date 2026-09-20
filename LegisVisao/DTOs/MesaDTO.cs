namespace LegisVisao.DTOs.MesaDTO
{
    public class MesaDTO
    {
        public int id { get; set; }
        public string uri { get; set; }
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string uriPartido { get; set; }
        public string siglaUf { get; set; }
        public int idLegislatura { get; set; }
        public string urlFoto { get; set; }
        public string dataInicio { get; set; }
        public string dataFim { get; set; }
        public string titulo { get; set; }
        public string codTitulo { get; set; }
    }

    public class Root
    {
        public List<MesaDTO> dados { get; set; }
        public List<Link> links { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }
}
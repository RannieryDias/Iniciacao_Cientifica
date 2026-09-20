namespace LegisVisao.DTOs
{
    public class AutorDTO
    {
        public int idProjeto { get; set; }
        public int codDeputado { get; set; }
        public string nome { get; set; }
        public int codTipo { get; set; }
        public string tipo { get; set; }
        public int ordemAssinatura { get; set; }
        public int proponente { get; set; }
    }

    public class AutoresDTO
    {
        public List<AutorDTO> autores { get; set; }
    }

    public class Root
    {
        public List<AutoresDTO> dados { get; set; }
        public List<Link> links { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }
}
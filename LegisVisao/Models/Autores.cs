namespace LegisVisao.Models
{
    public class Autor
    {
        public int idProjeto { get; set; }
        public int codDeputado { get; set; }
        public string nome { get; set; }
        public int codTipo { get; set; }
        public string tipo { get; set; }
        public int ordemAssinatura { get; set; }
        public int proponente { get; set; }
    }

    public class Autores
    {
        List<Autor> autores { get; set; }
    }
}

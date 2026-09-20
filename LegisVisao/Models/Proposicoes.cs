namespace LegisVisao.Models
{
    public class Proposicoes
    {
        IEnumerable<Proposicao> proposicoes;
    }

    public class Proposicao
    {
        public int id { get; set; }
        public string uri { get; set; }
        public string siglaTipo { get; set; }
        public int codTipo { get; set; }
        public int numero { get; set; }
        public int ano { get; set; }
        public string ementa { get; set; }
    }
}

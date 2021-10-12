using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models
{
    public class Autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }
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

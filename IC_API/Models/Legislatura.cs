using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models
{
    public class Legislatura
    {
        [Key]
        public int id { get; set; }
        public string uri { get; set; }
        public string dataInicio { get; set; }
        public string dataFim { get; set; }
    }
}

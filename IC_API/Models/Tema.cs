using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models
{
    public class Tema
    {
        [Key]
        public string cod { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
    }
}

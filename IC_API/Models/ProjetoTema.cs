using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models
{
    public class ProjetoTema
    {
        [Key]
        public int idProjeto { get; set; }
        [Key]
        public int idTema { get; set; }
    }
}

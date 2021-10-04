using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models
{
    public class Tramitacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id
        {
            get { return id; }
            set
            {
                id = int.Parse(projeto.id.ToString() + sequencia.ToString());
            }
        }
        public ProjetoDetalhado projeto { get; set; }
        public string dataHora { get; set; }
        public int sequencia { get; set; }
        public string siglaOrgao { get; set; }
        public string uriOrgao { get; set; }
        public string uriUltimoRelator { get; set; }
        public string regime { get; set; }
        public string descricaoTramitacao { get; set; }
        public string codTipoTramitacao { get; set; }
        public string descricaoSituacao { get; set; }
        public int? codSituacao { get; set; }
        public string despacho { get; set; }
         public string url { get; set; }
        public string ambito { get; set; }
    }
}

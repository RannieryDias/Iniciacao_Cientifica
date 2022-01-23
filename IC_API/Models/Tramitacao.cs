using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IC_API.Models
{
    public class Tramitacao
    {
        //[Key, Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int projetoId { get; set; }
        public string dataHora { get; set; }
        //[Key, Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
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

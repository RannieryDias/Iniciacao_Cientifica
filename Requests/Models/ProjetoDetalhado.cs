using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IC_DotNet.Models
{
    public class ProjetoDetalhado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string uri { get; set; }
        public string siglaTipo { get; set; }
        public int codTipo { get; set; }
        public int numero { get; set; }
        public int ano { get; set; }
        public string ementa { get; set; }
        public string dataApresentacao { get; set; }
        public string uriOrgaoNumerador { get; set; }
        [ForeignKey("statusId")]
        public StatusProposicao statusProposicao { get; set; }
        public string uriAutores { get; set; }
        public string descricaoTipo { get; set; }
        public string ementaDetalhada { get; set; }
        public string keywords { get; set; }
        public object uriPropPrincipal { get; set; }
        public object uriPropAnterior { get; set; }
        public object uriPropPosterior { get; set; }
        public string urlInteiroTeor { get; set; }
        public object urnFinal { get; set; }
        public object texto { get; set; }
        public object justificativa { get; set; }
    }
}

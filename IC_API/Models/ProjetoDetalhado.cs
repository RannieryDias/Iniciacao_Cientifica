﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models
{
    public class ProjetoDetalhado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        //talvez não
        public string uri { get; set; }
        public string siglaTipo { get; set; }
        //não
        public int codTipo { get; set; }
        public int numero { get; set; }
        public int ano { get; set; }
        public string ementa { get; set; }
        public string dataApresentacao { get; set; }
        public string uriAutores { get; set; }
        //nao
        public string descricaoTipo { get; set; }
        public string ementaDetalhada { get; set; }
        public string keywords { get; set; }
        public string uriPropPrincipal { get; set; }
        public string uriPropAnterior { get; set; }
        public string uriPropPosterior { get; set; }
        public string urlInteiroTeor { get; set; }
        public string urnFinal { get; set; }
        public string texto { get; set; }
        public string justificativa { get; set; }
        public bool codPlenario { get; set; }
        public bool codAprovado { get; set; }
        public bool apensado { get; set; }
        public bool arquivado { get; set; }
    }

    public class StatusProposicao
    {

        [Key]
        public int id { get; set; }
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

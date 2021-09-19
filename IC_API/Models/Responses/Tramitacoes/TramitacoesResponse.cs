using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IC_API.Models.Responses.Tramitacoes
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Dado
    {
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

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class TramitacoesResponse
    {
        public List<Dado> dados { get; set; }
        public List<Link> links { get; set; }
    }
}

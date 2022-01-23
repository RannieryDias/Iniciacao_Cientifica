using IC_API.Models;
using IC_API.Models.Responses.Autores_Response;
using IC_API.Models.Responses.ProjetoTema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class ProjetoTemaDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;


        public List<ProjetoTema> DeserializeProjetoTema(List<Projeto> projetos)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            List<ProjetoTema> projetoTemas = new List<ProjetoTema>();

            timer.Start();
            now = DateTime.Now;
            log.LogIt("***********************************");
            log.LogIt("Started to deserialize ProjetoTema at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            using (var webClient = new System.Net.WebClient())
            {

                int index = 0;

                foreach (var projeto in projetos)
                {
                    string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}/temas");
                    try
                    {
                        ProjetoTema projetoTema = new ProjetoTema();

                        ProjetoTemaResponse projetoTemaResponse = JsonConvert.DeserializeObject<ProjetoTemaResponse>(json, settings);

                        foreach (var response in projetoTemaResponse.dados)
                        {
                            projetoTema.idProjeto = projeto.id;
                            projetoTema.idTema = response.codTema;
                            projetoTemas.Add(projetoTema);
                            
                            if (projetoTemas.Count % 500 == 0)
                            {
                                log.LogIt(projetoTemas.Count + " Temas was deserialized! " +
                                    (projetos.Count - index) + " Projetos remaining" + " at " + now);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not parse response: " + projeto.id + "error: " + e.Message);
                    }

                    index++;
                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("The total of " + projetoTemas.Count + " Temas was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return projetoTemas;
        }
    }
}

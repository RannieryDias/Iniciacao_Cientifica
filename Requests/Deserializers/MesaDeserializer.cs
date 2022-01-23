using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses.MesaResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class MesaDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Mesa> DeserializeMesa(List<Legislatura> legislaturas)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.MesaResponse.Dado, Mesa>();
            });
            IMapper mapper = config.CreateMapper();

            List<Mesa> mesas = new List<Mesa>();

            timer.Start();
            now = DateTime.Now;
            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Legislaturas at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            using (var webClient = new System.Net.WebClient())
            {

                int index = 0;

                foreach (var legislatura in legislaturas)
                {
                    string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/legislaturas/{legislatura.id}/mesa");
                    try
                    {

                        MesaResponse MesaResponse = JsonConvert.DeserializeObject<MesaResponse>(json, settings);

                        foreach (var response in MesaResponse.dados)
                        {
                            Mesa mesa = mapper.Map<Mesa>(response);
                            mesas.Add(mesa);

                            if (mesas.Count % 500 == 0)
                            {
                                log.LogIt(mesas.Count + " Temas was deserialized! " +
                                    (legislaturas.Count - mesas.Count) + " Legislaturas remaining" + " at " + now);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not parse response: " + legislatura.id + "error: " + e.Message);
                    }

                    index++;
                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("The total of " + mesas.Count + " Temas was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return mesas;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IC_API.Models;
using Requests.Deserializers;
using Requests.Serializers;
using IC_API.Model;

namespace Requests
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instances of Classes
            //Deserializers
            //Teste teste = new Teste();
            //AutorDeserializer autorDeserializer = new AutorDeserializer();
            //Deserializer deserializer = new Deserializer();
            //ProjetoDetalhadoDeserializer projDes = new ProjetoDetalhadoDeserializer();
            //PartidoDeserializer parDes = new PartidoDeserializer();
            //DeputadoDeserializer deptDes = new DeputadoDeserializer();
            TramitacoesDeserializer tramitacoesDeserializer = new TramitacoesDeserializer();
            //Serializers
            //Serializer serializer = new Serializer();
            //ProjetoDetalhadoSerializer projDetSer = new ProjetoDetalhadoSerializer();
            //AutoresSerializer autoresSerializer = new AutoresSerializer();
            //DeputadoSerializer deputadoSerializer = new DeputadoSerializer();
            //PartidoSerializer partidoSerializer = new PartidoSerializer();

            ////Deserializing
            ////teste.Enviar();
            //List<Projeto> projetos = deserializer.DeserializeProjeto();
            //List<Autor> autores = autorDeserializer.DeserializeAutor(projetos);
            //List<ProjetoDetalhado> projDet = projDes.DeserializeProjetoDetalhado(projetos);
            //List<Deputado> deputados = deptDes.DeserializeDeputado();
            //List<Partido> partidos = parDes.DeserializePartido();

            ////Serializing
            //projDetSer.SerializeProjetoDetalhado(projDet);
            //autoresSerializer.SerializeAutor(autores);
            //deputadoSerializer.SerializeDeputado(deputados);
            //partidoSerializer.SerializePartido(partidos);

            //List<Autores> autores = deserializer.DeserializeProjetoAutores(projetos);
        }

    }
}

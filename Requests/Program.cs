using System.Collections.Generic;
using IC_API.Models;
using Requests.Deserializers;
using Requests.Serializers;
using System.Media;
using IC_API.Model;

namespace Requests
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instances of Classes
            //Deserializers
            Teste teste = new Teste();
            AutorDeserializer autorDeserializer = new AutorDeserializer();
            Deserializer deserializer = new Deserializer();
            ProjetoDetalhadoDeserializer projDes = new ProjetoDetalhadoDeserializer();
            PartidoDeserializer parDes = new PartidoDeserializer();
            DeputadoDeserializer deptDes = new DeputadoDeserializer();
            TramitacoesDeserializer tramitacoesDeserializer = new TramitacoesDeserializer();

            //Serializers
            GenericSerializer genericSerializer = new GenericSerializer();


            ////Deserializing
            //teste.Enviar();
            //List<ProjetoDetalhado> projDet = teste.Receber();
            List<Projeto> projetos = deserializer.DeserializeProjeto();
            List<ProjetoDetalhado> projDet = projDes.DeserializeProjetoDetalhado_NonIteractive(projetos);
            //List<Tramitacao> tramitacoes = tramitacoesDeserializer.DeserializeTramitacoes(ref projDet);
            //List<Autor> autores = autorDeserializer.DeserializeAutor(projetos);
            //List<Deputado> deputados = deptDes.DeserializeDeputado();
            //List<Partido> partidos = parDes.DeserializePartido();

            ////Serializing
            genericSerializer.SerializeProjetoDetalhado(projDet);
            //genericSerializer.SerializeProjetoDetalhado(tramitacoes);
            //genericSerializer.SerializeProjetoDetalhado(autores);
            //genericSerializer.SerializeProjetoDetalhado(deputados);
            //genericSerializer.SerializeProjetoDetalhado(partidos);
            //List<Autores> autores = deserializer.DeserializeProjetoAutores(projetos);
            //Console.WriteLine(projDet.Count);
#pragma warning disable CA1416 // Validate platform compatibility
            SystemSounds.Asterisk.Play();
#pragma warning restore CA1416 // Validate platform compatibility
        }

    }
}

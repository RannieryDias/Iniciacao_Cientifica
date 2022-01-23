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
            Logger log = new Logger();

            //Instances of Classes
            //Deserializers
            Teste teste = new Teste();
            AutorDeserializer autorDeserializer = new AutorDeserializer();
            Deserializer deserializer = new Deserializer();
            ProjetoDetalhadoDeserializer projDes = new ProjetoDetalhadoDeserializer();
            PartidoDeserializer parDes = new PartidoDeserializer();
            DeputadoDeserializer deptDes = new DeputadoDeserializer();
            TramitacoesDeserializer tramitacoesDeserializer = new TramitacoesDeserializer();
            TemaDeserializer temaDeserializer = new TemaDeserializer();
            ProjetoTemaDeserializer projetoTemaDeserializer = new ProjetoTemaDeserializer();
            LegislaturaDeserializer legislaturaDeserializer = new LegislaturaDeserializer();
            MesaDeserializer mesaDeserializer = new MesaDeserializer();

            //Serializers
            GenericSerializer genericSerializer = new GenericSerializer();


            ////Deserializing
            //teste.Enviar();
            //List<ProjetoDetalhado> projDet = teste.Receber();
            //int anoInicial = 2019;
            //int anoFinal = 2018;
            //for (; anoInicial > 1999; anoInicial--)
            //{
            //anoFinal = anoInicial - 1;
            //log.LogIt("***********************************");
            //log.LogIt("Starting progress of year: " + anoInicial + " until: " + anoFinal);
            //List<Projeto> projetos = deserializer.DeserializeProjeto(anoInicial, anoFinal);
            //List<ProjetoTema> projetoTemas = projetoTemaDeserializer.DeserializeProjetoTema(projetos);
            //}

            //List<Tema> temas = temaDeserializer.DeserializeTema();
            //List<ProjetoDetalhado> projDet = projDes.DeserializeProjetoDetalhado_NonIteractive(projetos);
            List<Tramitacao> tramitacoes = tramitacoesDeserializer.DeserializeTramitacoes(null);
            //List<Tramitacao> tramitacoes = tramitacoesDeserializer.DeserializeTramitacoes(projetos);
            //List<Tramitacao> tramitacoes = tramitacoesDeserializer.DeserializeTramitacoes(ref projDet);
            //List<Autor> autores = autorDeserializer.DeserializeAutor(projetos);
            //List<Deputado> deputados = deptDes.DeserializeDeputado();
            //List<Partido> partidos = parDes.DeserializePartido();
            //List<Legislatura> legislaturas = legislaturaDeserializer.DeserializeLegislatura();
            //List<Mesa> mesas = mesaDeserializer.DeserializeMesa(legislaturas);

            ////Serializing
            //genericSerializer.SerializeProjetoDetalhado(projDet);
            genericSerializer.SerializeProjetoDetalhado(tramitacoes);
            //genericSerializer.SerializeProjetoDetalhado(autores);
            //genericSerializer.SerializeProjetoDetalhado(deputados);
            //genericSerializer.SerializeProjetoDetalhado(partidos);
            //List<Autores> autores = deserializer.DeserializeProjetoAutores(projetos);
            //genericSerializer.SerializeProjetoDetalhado(temas);
            //genericSerializer.SerializeProjetoDetalhado(projetoTemas);
            //genericSerializer.SerializeProjetoDetalhado(legislaturas);
            //genericSerializer.SerializeProjetoDetalhado(mesas);

#pragma warning disable CA1416 // Validate platform compatibility
            SystemSounds.Asterisk.Play();
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}

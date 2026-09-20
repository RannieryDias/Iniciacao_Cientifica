using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text.Json;
using LegisVisao.DTOs;

namespace UnitTests
{
    [TestClass]
    public class LegisVisaoDtoModelTests
    {
        private IFixture _fixture;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture()
                .Customize(new AutoPropertiesCustomization())
                .Customize(new NullBehaviorCustomization());
        }

        [TestMethod]
        public void TestDeputadoDTO_Properties_ArePopulated()
        {
            // Arrange & Act
            var deputado = _fixture.Create<DeputadoDTO>();

            // Assert
            deputado.Should().NotBeNull();
            deputado.id.Should().BeGreaterThan(0);
            deputado.nomeCivil.Should().NotBeNullOrEmpty();
            deputado.uri.Should().NotBeNullOrEmpty();
            deputado.sexo.Should().NotBeNullOrEmpty();

            // Test nested UltimoStatusDTO
            if (deputado.ultimoStatus != null)
            {
                deputado.ultimoStatus.id.Should().BeGreaterThan(0);
                deputado.ultimoStatus.nome.Should().NotBeNullOrEmpty();
                deputado.ultimoStatus.siglaPartido.Should().NotBeNullOrEmpty();
            }
        }

        [TestMethod]
        public void TestMesaDTO_Properties_ArePopulated()
        {
            // Arrange & Act
            var mesa = _fixture.Create<MesaDTO>();

            // Assert
            mesa.Should().NotBeNull();
            mesa.id.Should().BeGreaterThan(0);
            mesa.nome.Should().NotBeNullOrEmpty();
            mesa.siglaPartido.Should().NotBeNullOrEmpty();
            mesa.siglaUf.Should().NotBeNullOrEmpty();
            mesa.dataInicio.Should().NotBeNull();
            mesa.dataFim.Should().NotBeNull();
            mesa.titulo.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void TestPartidoDTO_Properties_ArePopulated()
        {
            // Arrange & Act
            var partido = _fixture.Create<PartidoDTO>();

            // Assert
            partido.Should().NotBeNull();
            partido.id.Should().BeGreaterThan(0);
            partido.sigla.Should().NotBeNullOrEmpty();
            partido.nome.Should().NotBeNullOrEmpty();
            partido.uri.Should().NotBeNullOrEmpty();
            partido.urlLogo.Should().NotBeNullOrEmpty();
            partido.urlWebSite.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void TestProposicoesDTO_Properties_ArePopulated()
        {
            // Arrange & Act
            var autores = _fixture.Create<AutoresDTO>();

            // Assert
            autores.Should().NotBeNull();
            autores.autores.Should().NotBeNull();
            autores.autores.Should().HaveCountGreaterThan(0);

            // Test nested AutorDTO
            var autor = autores.autores.First();
            autor.idProjeto.Should().BeGreaterThan(0);
            autor.codDeputado.Should().BeGreaterThan(0);
            autor.nome.Should().NotBeNullOrEmpty();
            autor.codTipo.Should().BeGreaterThan(0);
            autor.tipo.Should().NotBeNullOrEmpty();
            autor.ordemAssinatura.Should().BeGreaterThan(0);
            autor.proponente.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void TestProjetoDetalhadoDTO_Properties_ArePopulated()
        {
            // Arrange & Act
            var projeto = _fixture.Create<ProjetoDetalhadoDTO>();

            // Assert
            projeto.Should().NotBeNull();
            projeto.id.Should().BeGreaterThan(0);
            projeto.uri.Should().NotBeNullOrEmpty();
            projeto.siglaTipo.Should().NotBeNullOrEmpty();
            projeto.codTipo.Should().BeGreaterThan(0);
            projeto.numero.Should().BeGreaterThan(0);
            projeto.ano.Should().BeGreaterThan(0);
            projeto.ementa.Should().NotBeNullOrEmpty();
            projeto.descricaoTipo.Should().NotBeNullOrEmpty();
            projeto.ementaDetalhada.Should().NotBeNullOrEmpty();
            projeto.keywords.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void TestModelSerialization_Deserialization_Success()
        {
            // Arrange
            var originalDeputado = new DeputadoDTO
            {
                id = 123,
                uri = "https://example.com/deputado/123",
                nomeCivil = "João Silva",
                cpf = "12345678901",
                sexo = "M",
                urlWebsite = "https://joaosilva.com",
                escolaridade = "Ensino Superior"
            };

            var originalMesa = new MesaDTO
            {
                id = 456,
                uri = "https://example.com/mesa/456",
                nome = "Mesa Diretora",
                siglaPartido = "PT",
                siglaUf = "SP"
            };

            // Act
            var serializedDeputado = JsonSerializer.Serialize(originalDeputado);
            var serializedMesa = JsonSerializer.Serialize(originalMesa);

            var deserializedDeputado = JsonSerializer.Deserialize<DeputadoDTO>(serializedDeputado);
            var deserializedMesa = JsonSerializer.Deserialize<MesaDTO>(serializedMesa);

            // Assert
            deserializedDeputado.Should().BeEquivalentTo(originalDeputado);
            deserializedMesa.Should().BeEquivalentTo(originalMesa);
        }

        [TestMethod]
        public void TestMultipleDTOs_Consistency_EachObjectIsIndependent()
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            var deputado1 = fixture.Create<DeputadoDTO>();
            var deputado2 = fixture.Create<DeputadoDTO>();
            var mesa1 = fixture.Create<MesaDTO>();
            var mesa2 = fixture.Create<MesaDTO>();

            // Assert - All objects should have unique IDs
            deputado1.id.Should().NotBe(deputado2.id);
            mesa1.id.Should().NotBe(mesa2.id);
        }

        [TestMethod]
        public void TestDTO_Data_Validation_ValidatesRequiredFields()
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            var deputado = fixture.Create<DeputadoDTO>();
            var mesa = fixture.Create<MesaDTO>();

            // Assert - Test that required fields are populated
            deputado.id.Should().BeGreaterThan(0);
            mesa.id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void TestModelType_DeputadoDTO_IsCorrect()
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            var deputado = fixture.Create<DeputadoDTO>();

            // Assert
            deputado.Should().BeOfType<DeputadoDTO>();
            typeof(DeputadoDTO).Should().HaveProperties()
                .Containing("id")
                .Containing("uri")
                .Containing("nomeCivil");
        }

        [TestMethod]
        public void TestModelType_MesaDTO_IsCorrect()
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            var mesa = fixture.Create<MesaDTO>();

            // Assert
            mesa.Should().BeOfType<MesaDTO>();
            typeof(MesaDTO).Should().HaveProperties()
                .Containing("id")
                .Containing("nome")
                .Containing("siglaPartido");
        }

        [TestMethod]
        public void TestModelType_PartidoDTO_IsCorrect()
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            var partido = fixture.Create<PartidoDTO>();

            // Assert
            partido.Should().BeOfType<PartidoDTO>();
            typeof(PartidoDTO).Should().HaveProperties()
                .Containing("id")
                .Containing("sigla")
                .Containing("nome");
        }

        [TestMethod]
        public void TestModelType_AutoresDTO_IsCorrect()
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            var autores = fixture.Create<AutoresDTO>();

            // Assert
            autores.Should().BeOfType<AutoresDTO>();
            typeof(AutoresDTO).Should().HaveProperties()
                .Containing("autores");
        }

        [TestMethod]
        public void TestModelType_ProjetoDetalhadoDTO_IsCorrect()
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            var projeto = fixture.Create<ProjetoDetalhadoDTO>();

            // Assert
            projeto.Should().BeOfType<ProjetoDetalhadoDTO>();
            typeof(ProjetoDetalhadoDTO).Should().HaveProperties()
                .Containing("id")
                .Containing("uri")
                .Containing("siglaTipo")
                .Containing("codTipo")
                .Containing("numero")
                .Containing("ano")
                .Containing("ementa");
        }
    }
}
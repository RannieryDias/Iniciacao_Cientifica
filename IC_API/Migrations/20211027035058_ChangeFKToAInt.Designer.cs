﻿// <auto-generated />
using System;
using IC_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IC_API.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20211027035058_ChangeFKToAInt")]
    partial class ChangeFKToAInt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("IC_API.Model.Deputado", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("cpf")
                        .HasColumnType("longtext");

                    b.Property<string>("dataFalecimento")
                        .HasColumnType("longtext");

                    b.Property<string>("dataNascimento")
                        .HasColumnType("longtext");

                    b.Property<string>("escolaridade")
                        .HasColumnType("longtext");

                    b.Property<string>("municipioNascimento")
                        .HasColumnType("longtext");

                    b.Property<string>("nomeCivil")
                        .HasColumnType("longtext");

                    b.Property<string>("sexo")
                        .HasColumnType("longtext");

                    b.Property<string>("ufNascimento")
                        .HasColumnType("longtext");

                    b.Property<int?>("ultimoStatusid")
                        .HasColumnType("int");

                    b.Property<string>("uri")
                        .HasColumnType("longtext");

                    b.Property<string>("urlWebsite")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("ultimoStatusid");

                    b.ToTable("Deputado");
                });

            modelBuilder.Entity("IC_API.Model.UltimoStatus", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("condicaoEleitoral")
                        .HasColumnType("longtext");

                    b.Property<string>("data")
                        .HasColumnType("longtext");

                    b.Property<string>("descricaoStatus")
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<int>("idLegislatura")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .HasColumnType("longtext");

                    b.Property<string>("nomeEleitoral")
                        .HasColumnType("longtext");

                    b.Property<string>("siglaPartido")
                        .HasColumnType("longtext");

                    b.Property<string>("siglaUf")
                        .HasColumnType("longtext");

                    b.Property<string>("situacao")
                        .HasColumnType("longtext");

                    b.Property<string>("uri")
                        .HasColumnType("longtext");

                    b.Property<string>("uriPartido")
                        .HasColumnType("longtext");

                    b.Property<string>("urlFoto")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("UltimoStatus");
                });

            modelBuilder.Entity("IC_API.Models.Autor", b =>
                {
                    b.Property<long>("id")
                        .HasColumnType("bigint");

                    b.Property<int>("codDeputado")
                        .HasColumnType("int");

                    b.Property<int>("codTipo")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .HasColumnType("longtext");

                    b.Property<int>("ordemAssinatura")
                        .HasColumnType("int");

                    b.Property<int>("proponente")
                        .HasColumnType("int");

                    b.Property<string>("tipo")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Autor");
                });

            modelBuilder.Entity("IC_API.Models.Lider", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("idLegislatura")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .HasColumnType("longtext");

                    b.Property<string>("siglaPartido")
                        .HasColumnType("longtext");

                    b.Property<string>("uf")
                        .HasColumnType("longtext");

                    b.Property<string>("uri")
                        .HasColumnType("longtext");

                    b.Property<string>("uriPartido")
                        .HasColumnType("longtext");

                    b.Property<string>("urlFoto")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Lider");
                });

            modelBuilder.Entity("IC_API.Models.Partido", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .HasColumnType("longtext");

                    b.Property<string>("numeroEleitoral")
                        .HasColumnType("longtext");

                    b.Property<string>("sigla")
                        .HasColumnType("longtext");

                    b.Property<int?>("statusid")
                        .HasColumnType("int");

                    b.Property<string>("uri")
                        .HasColumnType("longtext");

                    b.Property<string>("urlFacebook")
                        .HasColumnType("longtext");

                    b.Property<string>("urlLogo")
                        .HasColumnType("longtext");

                    b.Property<string>("urlWebSite")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("statusid");

                    b.ToTable("Partido");
                });

            modelBuilder.Entity("IC_API.Models.Projeto", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<int>("ano")
                        .HasColumnType("int");

                    b.Property<int>("codTipo")
                        .HasColumnType("int");

                    b.Property<string>("ementa")
                        .HasColumnType("longtext");

                    b.Property<int>("numero")
                        .HasColumnType("int");

                    b.Property<string>("siglaTipo")
                        .HasColumnType("longtext");

                    b.Property<string>("uri")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Projeto");
                });

            modelBuilder.Entity("IC_API.Models.ProjetoDetalhado", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<int>("ano")
                        .HasColumnType("int");

                    b.Property<bool>("codAprovado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("codPlenario")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("codTipo")
                        .HasColumnType("int");

                    b.Property<string>("dataApresentacao")
                        .HasColumnType("longtext");

                    b.Property<string>("descricaoTipo")
                        .HasColumnType("longtext");

                    b.Property<bool>("diff")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ementa")
                        .HasColumnType("longtext");

                    b.Property<string>("ementaDetalhada")
                        .HasColumnType("longtext");

                    b.Property<bool>("foiAPlenario")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("foiAprovado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("justificativa")
                        .HasColumnType("longtext");

                    b.Property<string>("keywords")
                        .HasColumnType("longtext");

                    b.Property<int>("numero")
                        .HasColumnType("int");

                    b.Property<string>("siglaTipo")
                        .HasColumnType("longtext");

                    b.Property<string>("texto")
                        .HasColumnType("longtext");

                    b.Property<string>("uri")
                        .HasColumnType("longtext");

                    b.Property<string>("uriAutores")
                        .HasColumnType("longtext");

                    b.Property<string>("uriPropAnterior")
                        .HasColumnType("longtext");

                    b.Property<string>("uriPropPosterior")
                        .HasColumnType("longtext");

                    b.Property<string>("uriPropPrincipal")
                        .HasColumnType("longtext");

                    b.Property<string>("urlInteiroTeor")
                        .HasColumnType("longtext");

                    b.Property<string>("urnFinal")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("ProjetoDetalhado");
                });

            modelBuilder.Entity("IC_API.Models.Status", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("data")
                        .HasColumnType("longtext");

                    b.Property<string>("idLegislatura")
                        .HasColumnType("longtext");

                    b.Property<int?>("liderid")
                        .HasColumnType("int");

                    b.Property<string>("situacao")
                        .HasColumnType("longtext");

                    b.Property<string>("totalMembros")
                        .HasColumnType("longtext");

                    b.Property<string>("totalPosse")
                        .HasColumnType("longtext");

                    b.Property<string>("uriMembros")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("liderid");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("IC_API.Models.StatusProposicao", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ambito")
                        .HasColumnType("longtext");

                    b.Property<int?>("codSituacao")
                        .HasColumnType("int");

                    b.Property<string>("codTipoTramitacao")
                        .HasColumnType("longtext");

                    b.Property<string>("dataHora")
                        .HasColumnType("longtext");

                    b.Property<string>("descricaoSituacao")
                        .HasColumnType("longtext");

                    b.Property<string>("descricaoTramitacao")
                        .HasColumnType("longtext");

                    b.Property<string>("despacho")
                        .HasColumnType("longtext");

                    b.Property<int?>("projetoid")
                        .HasColumnType("int");

                    b.Property<string>("regime")
                        .HasColumnType("longtext");

                    b.Property<int>("sequencia")
                        .HasColumnType("int");

                    b.Property<string>("siglaOrgao")
                        .HasColumnType("longtext");

                    b.Property<string>("uriOrgao")
                        .HasColumnType("longtext");

                    b.Property<string>("uriUltimoRelator")
                        .HasColumnType("longtext");

                    b.Property<string>("url")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("projetoid");

                    b.ToTable("StatusProposicao");
                });

            modelBuilder.Entity("IC_API.Models.Tramitacao", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("ambito")
                        .HasColumnType("longtext");

                    b.Property<int?>("codSituacao")
                        .HasColumnType("int");

                    b.Property<string>("codTipoTramitacao")
                        .HasColumnType("longtext");

                    b.Property<string>("dataHora")
                        .HasColumnType("longtext");

                    b.Property<string>("descricaoSituacao")
                        .HasColumnType("longtext");

                    b.Property<string>("descricaoTramitacao")
                        .HasColumnType("longtext");

                    b.Property<string>("despacho")
                        .HasColumnType("longtext");

                    b.Property<int>("projetoId")
                        .HasColumnType("int");

                    b.Property<string>("regime")
                        .HasColumnType("longtext");

                    b.Property<int>("sequencia")
                        .HasColumnType("int");

                    b.Property<string>("siglaOrgao")
                        .HasColumnType("longtext");

                    b.Property<string>("uriOrgao")
                        .HasColumnType("longtext");

                    b.Property<string>("uriUltimoRelator")
                        .HasColumnType("longtext");

                    b.Property<string>("url")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Tramitacao");
                });

            modelBuilder.Entity("IC_API.Model.Deputado", b =>
                {
                    b.HasOne("IC_API.Model.UltimoStatus", "ultimoStatus")
                        .WithMany()
                        .HasForeignKey("ultimoStatusid");

                    b.Navigation("ultimoStatus");
                });

            modelBuilder.Entity("IC_API.Models.Partido", b =>
                {
                    b.HasOne("IC_API.Models.Status", "status")
                        .WithMany()
                        .HasForeignKey("statusid");

                    b.Navigation("status");
                });

            modelBuilder.Entity("IC_API.Models.Status", b =>
                {
                    b.HasOne("IC_API.Models.Lider", "lider")
                        .WithMany()
                        .HasForeignKey("liderid");

                    b.Navigation("lider");
                });

            modelBuilder.Entity("IC_API.Models.StatusProposicao", b =>
                {
                    b.HasOne("IC_API.Models.ProjetoDetalhado", "projeto")
                        .WithMany()
                        .HasForeignKey("projetoid");

                    b.Navigation("projeto");
                });
#pragma warning restore 612, 618
        }
    }
}

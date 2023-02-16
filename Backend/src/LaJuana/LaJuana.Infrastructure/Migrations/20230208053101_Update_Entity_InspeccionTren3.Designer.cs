﻿// <auto-generated />
using System;
using LaJuana.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LaJuana.Infrastructure.Migrations
{
    [DbContext(typeof(LaJuanaDbContext))]
    [Migration("20230208053101_Update_Entity_InspeccionTren3")]
    partial class Update_Entity_InspeccionTren3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LaJuana.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LaJuana.Domain.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhotoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("SubCategoryId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("LaJuana.Domain.InspectionTrain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AspectoBasicoCuatroNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoCuatroObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoCuatroSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoDosNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoDosObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoDosSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoTresNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoTresObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoTresSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoUnoNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoUnoObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoBasicoUnoSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoCincoNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoCincoObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoCincoSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoDiezNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoDiezObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoDiezSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoNueveNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoNueveObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoNueveSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoOchoNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoOchoObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoOchoSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoSeisNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoSeisObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoSeisSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoSieteNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoSieteObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AspectoTecnicoSieteSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuxiliarMaquina")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenCatorceItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenCatorceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenCatorceObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenCatorceSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciNueveItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciNueveNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciNueveObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciNueveSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciOchoItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciOchoNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciOchoObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciOchoSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciSeisItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciSeisNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciSeisObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciSeisSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciSieteItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciSieteNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciSieteObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenDieciSieteSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenQuinceItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenQuinceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenQuinceObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenQuinceSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeinteItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeinteNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeinteObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeinteSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiDosItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiDosNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiDosObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiDosSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiTresItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiTresNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiTresObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiTresSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiUnoItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiUnoNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiUnoObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspeccionTrenVeintiUnoSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Locomotoras")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maquinista")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoDoceObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoDoceSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoDoceeNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoOnceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoOnceObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoOnceSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoTreceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoTreceObservacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenejoAdecuadoTreceSi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroTren")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObservacionEvaluador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("SubCategoryId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("InspectionTrains");
                });

            modelBuilder.Entity("LaJuana.Domain.Program", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("LaJuana.Domain.Category", b =>
                {
                    b.HasOne("LaJuana.Domain.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("LaJuana.Domain.Document", b =>
                {
                    b.HasOne("LaJuana.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaJuana.Domain.Category", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("LaJuana.Domain.InspectionTrain", b =>
                {
                    b.HasOne("LaJuana.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaJuana.Domain.Category", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("SubCategory");
                });
#pragma warning restore 612, 618
        }
    }
}

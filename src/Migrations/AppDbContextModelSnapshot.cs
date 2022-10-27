﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjControleEstoque.Context;

#nullable disable

namespace ProjControleEstoque.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjControleEstoque.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Criado")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("EstrategiaConsumo")
                        .HasColumnType("longtext");

                    b.Property<int?>("FornecedorId")
                        .HasColumnType("int");

                    b.Property<string>("Localizacao")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Validade")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProjControleEstoque.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Criado")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CriadoPorId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Endereco")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ProjControleEstoque.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Funcao")
                        .HasColumnType("longtext");

                    b.Property<string>("Hash")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjControleEstoque.Models.Product", b =>
                {
                    b.HasOne("ProjControleEstoque.Models.Supplier", "Fornecedor")
                        .WithMany("Products")
                        .HasForeignKey("FornecedorId");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("ProjControleEstoque.Models.Supplier", b =>
                {
                    b.HasOne("ProjControleEstoque.Models.User", "CriadoPor")
                        .WithMany()
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CriadoPor");
                });

            modelBuilder.Entity("ProjControleEstoque.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}

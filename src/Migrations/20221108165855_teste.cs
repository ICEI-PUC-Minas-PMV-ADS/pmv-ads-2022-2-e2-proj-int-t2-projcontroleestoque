
﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleEstoque.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8");

            // TABELA: usuario.
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Hash = table.Column<string>(type: "varchar(200)", nullable: false),
                    Funcao = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TIMESTAMP", defaultValueSql: "CURRENT_TIMESTAMP"),
                    Email = table.Column<string>(type: "varchar(200)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8");


            // TABELA: fornecedor.
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)"),
                    Endereco = table.Column<string>(type: "varchar(100)"),
                    Telefone = table.Column<string>(type: "varchar(50)"),
                    Email = table.Column<string>(type: "varchar(200)"),
                    Criado = table.Column<DateTime>(type: "TIMESTAMP", defaultValueSql: "CURRENT_TIMESTAMP"),
                    CriadoPorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);

                })
                .Annotation("MySql:CharSet", "utf8");

            // TABELA: Products.
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)"),
                    Quantidade = table.Column<int>(type: "int", defaultValue: 0),
                    FornecedorId = table.Column<int>(type: "int", nullable: true),
                    Localizacao = table.Column<string>(type: "varchar(200)"),
                    Tags = table.Column<string>(type: "varchar(200)"),
                    EstrategiaConsumo = table.Column<string>(type: "varchar(200)"),
                    Criado = table.Column<DateTime>(type: "TIMESTAMP", defaultValueSql: "CURRENT_TIMESTAMP"),
                    Validade = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8");

            // TABELA: MovimentacaoEstoques.
            migrationBuilder.CreateTable(
                name: "MovimentacaoEstoques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Motivo = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    RegistradoPorId = table.Column<int>(type: "int", nullable: true),
                    SolicitadoPorId = table.Column<int>(type: "int", nullable: true),
                    ProdutoId = table.Column<int>(type: "int", nullable: true),
                    DataMovimento = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoEstoques", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            // TABELA: Inventario.
            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RealizadoPorId = table.Column<int>(type: "int", nullable: true),
                    SolicitadoPorId = table.Column<int>(type: "int", nullable: true),
                    DataDeExecucao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            // TABELA: ItemInventario.
            migrationBuilder.CreateTable(
                name: "ItemInventarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InventarioId = table.Column<int>(type: "int", nullable: true),
                    ProdutoId = table.Column<int>(type: "int", nullable: true),
                    Observacao = table.Column<string>(type: "varchar(300)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInventario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            // TABELA: ItemInventario.
            migrationBuilder.CreateTable(
                name: "AgendaInventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InventarioId = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Agendamento = table.Column<string>(type: "varchar(500)", nullable: true),
                    SolicitadoPorId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaInventario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            // CHAVE ESTRANGEIRA: Suppliers
            migrationBuilder.AddForeignKey(
                "FK_Suppliers_User",    // Nome
                "Suppliers",            // Tabela
                "CriadoPorId",          // Column estrangeira
                "Users",                // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            // CHAVE ESTRANGEIRA: Products
            migrationBuilder.AddForeignKey(
                "FK_Products_Supplier", // Nome
                "Products",             // Tabela
                "FornecedorId",         // Column estrangeira
                "Suppliers",            // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            // CHAVE ESTRANGEIRA: MovimentacaoEstoques
            migrationBuilder.AddForeignKey(
                "FK_MovimentacaoEstoques_Product",   // Nome
                "MovimentacaoEstoques",              // Tabela
                "ProdutoId",                         // Column estrangeira
                "Products",                          // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_MovimentacaoEstoques_User", // Nome
                "MovimentacaoEstoques",         // Tabela
                "RegistradoPorId",              // Column estrangeira
                "Users",                        // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_MovimentacaoEstoques_Users_SolicitadoPorId",
                "MovimentacaoEstoques",
                "SolicitadoPorId",
                "Users",
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            // CHAVE ESTRANGEIRA: Inventario
            migrationBuilder.AddForeignKey(
                "FK_Inventario_RealizadoPor",   // Nome
                "Inventarios",                  // Tabela
                "RealizadoPorId",               // Column estrangeira
                "Users",                        // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_Inventario_SolicitadoPor",  // Nome
                "Inventarios",                  // Tabela
                "SolicitadoPorId",              // Column estrangeira
                "Users",                        // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            // CHAVE ESTRANGEIRA: ItemInventario
            migrationBuilder.AddForeignKey(
                "FK_ItemInventario_Inventario", // Nome
                "ItemInventarios",              // Tabela
                "InventarioId",                 // Column estrangeira
                "Inventarios",                  // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_ItemInventario_Produto",    // Nome
                "ItemInventarios",              // Tabela
                "ProdutoId",                    // Column estrangeira
                "Products",                     // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            // CHAVE ESTRANGEIRA: AgendaInventario
            migrationBuilder.AddForeignKey(
                "FK_AgendaInventario_Inventario", // Nome
                "AgendaInventario",               // Tabela
                "InventarioId",                   // Column estrangeira
                "Inventarios",                    // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_AgendaInventario_SolicitadoPor", // Nome
                "AgendaInventario",                  // Tabela
                "SolicitadoPorId",                   // Column estrangeira
                "Users",                             // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_MovimentacaoEstoques_Users_SolicitadoPorId", "MovimentacaoEstoques");
            migrationBuilder.DropForeignKey("FK_MovimentacaoEstoques_Users_RegistradoPorId", "MovimentacaoEstoques");
            migrationBuilder.DropForeignKey("FK_MovimentacaoEstoques_Products_ProdutoId", "MovimentacaoEstoques");

            migrationBuilder.DropForeignKey("FK_ProductsSuppliers", "Products");

            migrationBuilder.DropForeignKey("FK_SuppliersUsers", "Suppliers");

            migrationBuilder.DropForeignKey("FK_Inventario_RealizadoPor", "Inventarios");
            migrationBuilder.DropForeignKey("FK_Inventario_SolicitadoPor", "Inventarios");

            migrationBuilder.DropForeignKey("FK_ItemInventario_Inventario", "ItemInventarios");
            migrationBuilder.DropForeignKey("FK_ItemInventario_Produto", "ItemInventarios");

            migrationBuilder.DropForeignKey("FK_AgendaInventario_SolicitadoPor", "AgendaInventario");
            migrationBuilder.DropForeignKey("FK_AgendaInventario_Inventario", "AgendaInventario");

            migrationBuilder.DropTable(
                name: "AgendaInventario");

            migrationBuilder.DropTable(
                name: "ItemInventarios");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "MovimentacaoEstoques");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

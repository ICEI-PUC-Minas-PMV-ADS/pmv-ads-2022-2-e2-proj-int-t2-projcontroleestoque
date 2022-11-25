using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleEstoque.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealizadoPorId = table.Column<int>(type: "int", nullable: false),
                    SolicitadoPorId = table.Column<int>(type: "int", nullable: false),
                    DataDeExecucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventarios_Users_RealizadoPorId",
                        column: x => x.RealizadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Inventarios_Users_SolicitadoPorId",
                        column: x => x.SolicitadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Users_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    FornecedorId = table.Column<int>(type: "int", nullable: true),
                    Localizacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstrategiaConsumo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemInventarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventarioId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInventarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInventarios_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemInventarios_Products_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movimentacaoEstoques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    RegistradoPorId = table.Column<int>(type: "int", nullable: false),
                    SolicitadoPorId = table.Column<int>(type: "int", nullable: true),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimentacaoEstoques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movimentacaoEstoques_Products_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movimentacaoEstoques_Users_RegistradoPorId",
                        column: x => x.RegistradoPorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movimentacaoEstoques_Users_SolicitadoPorId",
                        column: x => x.SolicitadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_RealizadoPorId",
                table: "Inventarios",
                column: "RealizadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_SolicitadoPorId",
                table: "Inventarios",
                column: "SolicitadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventarios_InventarioId",
                table: "ItemInventarios",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventarios_ProdutoId",
                table: "ItemInventarios",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacaoEstoques_ProdutoId",
                table: "movimentacaoEstoques",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacaoEstoques_RegistradoPorId",
                table: "movimentacaoEstoques",
                column: "RegistradoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacaoEstoques_SolicitadoPorId",
                table: "movimentacaoEstoques",
                column: "SolicitadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FornecedorId",
                table: "Products",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CriadoPorId",
                table: "Suppliers",
                column: "CriadoPorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemInventarios");

            migrationBuilder.DropTable(
                name: "movimentacaoEstoques");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");

        }
    }
}

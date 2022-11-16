using System;
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

            // TABELA: fornecedor.
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

            // TABELA: movimentacao estoque.
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


            // CHAVES ESTANGEIRAS
            migrationBuilder.AddForeignKey(
                "FK_SuppliersUsers",    // Nome
                "Suppliers",            // Tabela
                "CriadoPorId",          // Column estrangeira
                "Users",                // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_ProductsSuppliers", // Nome
                "Products",             // Tabela
                "FornecedorId",         // Column estrangeira
                "Suppliers",            // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_MovimentacaoEstoques_Products_ProdutoId",
                "MovimentacaoEstoques",
                "ProdutoId",
                "Products",
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_MovimentacaoEstoques_Users_RegistradoPorId",
                "MovimentacaoEstoques",
                "RegistradoPorId",
                "Users",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_MovimentacaoEstoques_Users_SolicitadoPorId", "MovimentacaoEstoques");
            migrationBuilder.DropForeignKey("FK_MovimentacaoEstoques_Users_RegistradoPorId", "MovimentacaoEstoques");
            migrationBuilder.DropForeignKey("FK_MovimentacaoEstoques_Products_ProdutoId", "MovimentacaoEstoques");

            migrationBuilder.DropForeignKey("FK_ProductsSuppliers", "Products");

            migrationBuilder.DropForeignKey("FK_SuppliersUsers", "Suppliers");

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

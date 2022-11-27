using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleEstoque.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            UpMySQL(migrationBuilder);
            //UpSQLServer(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            DownMySQL(migrationBuilder);
            //DownSQLServer(migrationBuilder);
        }

        protected void UpMySQL(MigrationBuilder migrationBuilder)
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
                name: "AgendaInventarios",
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
                    table.PrimaryKey("PK_AgendaInventarios", x => x.Id);
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
                "FK_AgendaInventarios_Inventario", // Nome
                "AgendaInventarios",               // Tabela
                "InventarioId",                   // Column estrangeira
                "Inventarios",                    // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                "FK_AgendaInventarios_SolicitadoPor", // Nome
                "AgendaInventarios",                  // Tabela
                "SolicitadoPorId",                   // Column estrangeira
                "Users",                             // Tabela estrangeira
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull);
        }

        protected void DownMySQL(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey("FK_AgendaInventarios_SolicitadoPor", "AgendaInventarios");
            migrationBuilder.DropForeignKey("FK_AgendaInventarios_Inventario", "AgendaInventarios");

            migrationBuilder.DropTable(
                name: "AgendaInventarios");

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

        protected void UpSQLServer(MigrationBuilder migrationBuilder)
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

        protected void DownSQLServer(MigrationBuilder migrationBuilder)
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

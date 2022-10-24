using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleEstoque.Migrations
{
    public partial class product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

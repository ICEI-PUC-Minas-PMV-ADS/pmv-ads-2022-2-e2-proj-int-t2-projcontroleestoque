using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjControleEstoque.Migrations
{
    public partial class SetupForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey("FK_SuppliersUsers", "Suppliers", "CriadoPorId", "Users", principalColumn: "Id", onUpdate: ReferentialAction.Cascade, onDelete: ReferentialAction.SetNull);
            migrationBuilder.AddForeignKey("FK_ProductsSuppliers", "Products", "FornecedorId", "Suppliers", principalColumn: "Id", onUpdate: ReferentialAction.Cascade, onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_ProductsSuppliers", "Products");
            migrationBuilder.DropForeignKey("FK_SuppliersUsers", "Suppliers");
        }
    }
}

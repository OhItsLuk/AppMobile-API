using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "produtos");

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                table: "produtos");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "produtos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseMvcApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrigemClienteToCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrigemCliente",
                table: "Clientes",
                type: "varchar(255)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrigemCliente",
                table: "Clientes");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financeasy.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFinancialEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "financial_entry",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFixed",
                table: "financial_entry",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "financial_entry");

            migrationBuilder.DropColumn(
                name: "IsFixed",
                table: "financial_entry");
        }
    }
}

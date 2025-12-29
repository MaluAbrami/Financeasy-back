using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financeasy.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdInUpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "updates",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "updates");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewCall_Api.Migrations
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "Admins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "passwordHash",
                table: "Admins",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "passwordSalt",
                table: "Admins",
                type: "TEXT",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewCall_Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Students_studentid",
                table: "Absences");

            migrationBuilder.DropIndex(
                name: "IX_Absences_studentid",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "studentid",
                table: "Absences",
                newName: "studentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Absences",
                newName: "studentid");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_studentid",
                table: "Absences",
                column: "studentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Students_studentid",
                table: "Absences",
                column: "studentid",
                principalTable: "Students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

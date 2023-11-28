using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewCall_Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Students_studentId",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Absences",
                newName: "studentid");

            migrationBuilder.RenameIndex(
                name: "IX_Absences_studentId",
                table: "Absences",
                newName: "IX_Absences_studentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Students_studentid",
                table: "Absences",
                column: "studentid",
                principalTable: "Students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Students_studentid",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "studentid",
                table: "Absences",
                newName: "studentId");

            migrationBuilder.RenameIndex(
                name: "IX_Absences_studentid",
                table: "Absences",
                newName: "IX_Absences_studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Students_studentId",
                table: "Absences",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

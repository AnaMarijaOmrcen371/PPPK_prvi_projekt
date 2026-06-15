using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixUniqueOib : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_OIB",
                table: "Patients",
                column: "OIB",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_OIB",
                table: "Patients");
        }
    }
}

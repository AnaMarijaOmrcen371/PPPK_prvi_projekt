using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddExaminationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExaminationType",
                table: "Appointments",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExaminationType",
                table: "Appointments");
        }
    }
}

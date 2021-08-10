using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonation.Data.Migrations
{
    public partial class RefactoringAppointmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hospital",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "HospitalCity",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "HospitalWard",
                table: "Appointments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hospital",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCity",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalWard",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

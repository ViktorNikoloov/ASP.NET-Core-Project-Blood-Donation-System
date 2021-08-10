using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonation.Data.Migrations
{
    public partial class ReformingAppointmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "SentBy",
                table: "Appointments",
                newName: "Hospital");

            migrationBuilder.RenameColumn(
                name: "PublishedOn",
                table: "Appointments",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Appointments",
                newName: "AdditionalInfo");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Appointments",
                newName: "PublishedOn");

            migrationBuilder.RenameColumn(
                name: "Hospital",
                table: "Appointments",
                newName: "SentBy");

            migrationBuilder.RenameColumn(
                name: "AdditionalInfo",
                table: "Appointments",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Appointments",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Appointments",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Appointments",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

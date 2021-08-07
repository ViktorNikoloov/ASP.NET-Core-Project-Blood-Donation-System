using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonation.Data.Migrations
{
    public partial class AddLastDonationColumnToDonor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PostCode",
                table: "Towns",
                type: "int",
                maxLength: 9999,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 9999);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDonation",
                table: "Donors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDonation",
                table: "Donors");

            migrationBuilder.AlterColumn<int>(
                name: "PostCode",
                table: "Towns",
                type: "int",
                maxLength: 9999,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 9999,
                oldNullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonation.Data.Migrations
{
    public partial class RemoveCountriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropColumn(
                name: "HospitalCity",
                table: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Addresses",
                newName: "TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                newName: "IX_Addresses_TownId");

            migrationBuilder.AddColumn<int>(
                name: "TownId",
                table: "Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_TownId",
                table: "Hospitals",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Towns_TownId",
                table: "Addresses",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_Towns_TownId",
                table: "Hospitals",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Towns_TownId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_Towns_TownId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_TownId",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "TownId",
                table: "Addresses",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_TownId",
                table: "Addresses",
                newName: "IX_Addresses_CountryId");

            migrationBuilder.AddColumn<string>(
                name: "HospitalCity",
                table: "Hospitals",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsDeleted",
                table: "Countries",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_TownId",
                table: "Countries",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

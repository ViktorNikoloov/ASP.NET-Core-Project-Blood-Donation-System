using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonation.Data.Migrations
{
    public partial class AddStretTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "Towns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Towns_StreetId",
                table: "Towns",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_IsDeleted",
                table: "Streets",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Streets_StreetId",
                table: "Towns",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Streets_StreetId",
                table: "Towns");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Towns_StreetId",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Towns");
        }
    }
}

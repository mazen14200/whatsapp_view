using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bnan.Inferastructure.Migrations
{
    public partial class IgnoreColOfUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "CR_Mas_User_Information");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CR_Mas_User_Information");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "CR_Mas_User_Information");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "CR_Mas_User_Information");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CR_Mas_User_Information");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "CR_Mas_User_Information");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CR_Mas_User_Information",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "CR_Mas_User_Information",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "CR_Mas_User_Information",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CR_Mas_User_Information",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "CR_Mas_User_Information",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "CR_Mas_User_Information",
                column: "NormalizedEmail");
        }
    }
}

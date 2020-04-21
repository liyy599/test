using Microsoft.EntityFrameworkCore.Migrations;

namespace ZC.Customer.Repository.Migrations
{
    public partial class UpdateModel4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "StaffInfo");

            migrationBuilder.DropColumn(
                name: "CreatorUserName",
                table: "StaffInfo");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "StaffInfo");

            migrationBuilder.DropColumn(
                name: "LastModifierUserName",
                table: "StaffInfo");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ProductInfo");

            migrationBuilder.DropColumn(
                name: "CreatorUserName",
                table: "ProductInfo");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "ProductInfo");

            migrationBuilder.DropColumn(
                name: "LastModifierUserName",
                table: "ProductInfo");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CreatorUserName",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "LastModifierUserName",
                table: "CompanyInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "StaffInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserName",
                table: "StaffInfo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "StaffInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserName",
                table: "StaffInfo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "ProductInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserName",
                table: "ProductInfo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "ProductInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserName",
                table: "ProductInfo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserName",
                table: "CompanyInfo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserName",
                table: "CompanyInfo",
                maxLength: 50,
                nullable: true);
        }
    }
}

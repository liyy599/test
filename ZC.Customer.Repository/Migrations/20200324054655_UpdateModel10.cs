using Microsoft.EntityFrameworkCore.Migrations;

namespace ZC.Customer.Repository.Migrations
{
    public partial class UpdateModel10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractAddress",
                table: "CompanyInfo",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractName",
                table: "CompanyInfo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNum",
                table: "CompanyInfo",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractAddress",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "ContractName",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "ContractNum",
                table: "CompanyInfo");
        }
    }
}

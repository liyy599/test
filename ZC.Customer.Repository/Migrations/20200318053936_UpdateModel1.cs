using Microsoft.EntityFrameworkCore.Migrations;

namespace ZC.Customer.Repository.Migrations
{
    public partial class UpdateModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SalesmanName",
                table: "CompanyInfo",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "DirectorName",
                table: "CompanyInfo",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SalesmanName",
                table: "CompanyInfo",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "DirectorName",
                table: "CompanyInfo",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}

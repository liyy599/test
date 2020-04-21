using Microsoft.EntityFrameworkCore.Migrations;

namespace ZC.Customer.Repository.Migrations
{
    public partial class UpdateModel7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "FileUploadsInfo",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "FileUploadsInfo",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}

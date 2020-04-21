using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZC.Customer.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: false),
                    CustomerType = table.Column<long>(nullable: false),
                    CustomerLevel = table.Column<long>(nullable: false),
                    Area = table.Column<int>(nullable: false),
                    Province = table.Column<long>(nullable: false),
                    DirectorId = table.Column<long>(nullable: false),
                    DirectorName = table.Column<long>(nullable: false),
                    SalesmanId = table.Column<long>(nullable: false),
                    SalesmanName = table.Column<long>(nullable: false),
                    Express = table.Column<int>(nullable: false),
                    Invoice = table.Column<int>(nullable: false),
                    Template = table.Column<int>(nullable: false),
                    LinkNum = table.Column<int>(nullable: false),
                    IsSeal = table.Column<bool>(nullable: false),
                    QT = table.Column<int>(nullable: false),
                    ColdChain = table.Column<int>(nullable: false),
                    AlterationCompany = table.Column<string>(maxLength: 100, nullable: false),
                    AffiliatedCompany = table.Column<string>(maxLength: 100, nullable: false),
                    SpecialProduct = table.Column<string>(nullable: false),
                    ExtraPacking = table.Column<string>(nullable: false),
                    AccountPeriod = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreatorUserName = table.Column<string>(maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserName = table.Column<string>(maxLength: 50, nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(maxLength: 100, nullable: false),
                    Specs = table.Column<string>(maxLength: 50, nullable: true),
                    Model = table.Column<string>(maxLength: 50, nullable: true),
                    Abbreviation = table.Column<string>(maxLength: 50, nullable: true),
                    Pack = table.Column<string>(maxLength: 50, nullable: true),
                    Milliliter = table.Column<string>(maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Rise = table.Column<string>(nullable: true),
                    RegisterNo = table.Column<string>(maxLength: 50, nullable: true),
                    CreatorUserName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserName = table.Column<string>(maxLength: 50, nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInfo");

            migrationBuilder.DropTable(
                name: "ProductInfo");
        }
    }
}

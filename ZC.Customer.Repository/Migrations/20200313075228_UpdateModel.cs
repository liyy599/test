using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZC.Customer.Repository.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    StaffName = table.Column<string>(maxLength: 50, nullable: false),
                    Post = table.Column<string>(nullable: false),
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
                    table.PrimaryKey("PK_StaffInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffInfo");
        }
    }
}

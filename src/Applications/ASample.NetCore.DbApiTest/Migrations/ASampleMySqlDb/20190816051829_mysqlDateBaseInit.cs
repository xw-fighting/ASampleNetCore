using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASample.NetCore.DbApiTest.Migrations.ASampleMySqlDb
{
    public partial class mysqlDateBaseInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Birthday = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });
        }
    }
}

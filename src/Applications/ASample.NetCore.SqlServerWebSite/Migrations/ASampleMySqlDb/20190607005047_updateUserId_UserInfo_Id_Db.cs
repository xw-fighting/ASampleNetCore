using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASample.NetCore.SqlServerWebSite.Migrations.ASampleMySqlDb
{
    public partial class updateUserId_UserInfo_Id_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                nullable: false,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserInfo",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(byte[]));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Users",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "UserInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");
        }
    }
}

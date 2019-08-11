using Microsoft.EntityFrameworkCore.Migrations;

namespace ASample.NetCore.Auths.Migrations
{
    public partial class updateRightFiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TUser",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RightIcon",
                table: "TRight",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RightUrl",
                table: "TRight",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RightIcon",
                table: "TRight");

            migrationBuilder.DropColumn(
                name: "RightUrl",
                table: "TRight");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TUser",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}

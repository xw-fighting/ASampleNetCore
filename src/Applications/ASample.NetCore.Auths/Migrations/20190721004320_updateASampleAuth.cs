using Microsoft.EntityFrameworkCore.Migrations;

namespace ASample.NetCore.Auths.Migrations
{
    public partial class updateASampleAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "TUser",
                newName: "PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "TUser",
                newName: "Phone");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleSocialNetwork.Migrations
{
    public partial class blocked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLogin",
                table: "AspNetUsers",
                newName: "IsBlocked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "AspNetUsers",
                newName: "IsLogin");
        }
    }
}

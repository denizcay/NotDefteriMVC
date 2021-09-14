using Microsoft.EntityFrameworkCore.Migrations;

namespace NotDefteriMVC.Data.Migrations
{
    public partial class PhotoField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "AspNetUsers",
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "AspNetUsers");
        }
    }
}

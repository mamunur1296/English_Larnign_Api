using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    public partial class addFormId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormsId",
                table: "SentenceStructures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isAssaindByforms",
                table: "SentenceStructures",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isAssaindBySubCatagory",
                table: "SentenceFormss",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormsId",
                table: "SentenceStructures");

            migrationBuilder.DropColumn(
                name: "isAssaindByforms",
                table: "SentenceStructures");

            migrationBuilder.DropColumn(
                name: "isAssaindBySubCatagory",
                table: "SentenceFormss");
        }
    }
}

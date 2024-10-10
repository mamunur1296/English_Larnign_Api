using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SentenceFormss_SubCategories_SubCategoryId",
                table: "SentenceFormss");

            migrationBuilder.DropForeignKey(
                name: "FK_SentenceStructures_SentenceFormss_SentenceFormId",
                table: "SentenceStructures");

            migrationBuilder.DropIndex(
                name: "IX_SentenceStructures_SentenceFormId",
                table: "SentenceStructures");

            migrationBuilder.DropIndex(
                name: "IX_SentenceFormss_SubCategoryId",
                table: "SentenceFormss");

            migrationBuilder.DropColumn(
                name: "SentenceFormId",
                table: "SentenceStructures");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "SentenceFormss");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SentenceFormId",
                table: "SentenceStructures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubCategoryId",
                table: "SentenceFormss",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SentenceStructures_SentenceFormId",
                table: "SentenceStructures",
                column: "SentenceFormId");

            migrationBuilder.CreateIndex(
                name: "IX_SentenceFormss_SubCategoryId",
                table: "SentenceFormss",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SentenceFormss_SubCategories_SubCategoryId",
                table: "SentenceFormss",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SentenceStructures_SentenceFormss_SentenceFormId",
                table: "SentenceStructures",
                column: "SentenceFormId",
                principalTable: "SentenceFormss",
                principalColumn: "Id");
        }
    }
}

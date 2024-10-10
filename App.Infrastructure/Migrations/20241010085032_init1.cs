using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubCategoryId",
                table: "SubCategoryFormMappings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SentenceFormId",
                table: "SubCategoryFormMappings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SentenceFormId",
                table: "FormStructureMappings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentenceStructureId",
                table: "FormStructureMappings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryFormMappings_SentenceFormId",
                table: "SubCategoryFormMappings",
                column: "SentenceFormId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryFormMappings_SubCategoryId",
                table: "SubCategoryFormMappings",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FormStructureMappings_SentenceFormId",
                table: "FormStructureMappings",
                column: "SentenceFormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormStructureMappings_SentenceStructureId",
                table: "FormStructureMappings",
                column: "SentenceStructureId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormStructureMappings_SentenceFormss_SentenceFormId",
                table: "FormStructureMappings",
                column: "SentenceFormId",
                principalTable: "SentenceFormss",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormStructureMappings_SentenceStructures_SentenceStructureId",
                table: "FormStructureMappings",
                column: "SentenceStructureId",
                principalTable: "SentenceStructures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryFormMappings_SentenceFormss_SentenceFormId",
                table: "SubCategoryFormMappings",
                column: "SentenceFormId",
                principalTable: "SentenceFormss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryFormMappings_SubCategories_SubCategoryId",
                table: "SubCategoryFormMappings",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormStructureMappings_SentenceFormss_SentenceFormId",
                table: "FormStructureMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_FormStructureMappings_SentenceStructures_SentenceStructureId",
                table: "FormStructureMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryFormMappings_SentenceFormss_SentenceFormId",
                table: "SubCategoryFormMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryFormMappings_SubCategories_SubCategoryId",
                table: "SubCategoryFormMappings");

            migrationBuilder.DropIndex(
                name: "IX_SubCategoryFormMappings_SentenceFormId",
                table: "SubCategoryFormMappings");

            migrationBuilder.DropIndex(
                name: "IX_SubCategoryFormMappings_SubCategoryId",
                table: "SubCategoryFormMappings");

            migrationBuilder.DropIndex(
                name: "IX_FormStructureMappings_SentenceFormId",
                table: "FormStructureMappings");

            migrationBuilder.DropIndex(
                name: "IX_FormStructureMappings_SentenceStructureId",
                table: "FormStructureMappings");

            migrationBuilder.DropColumn(
                name: "SentenceFormId",
                table: "FormStructureMappings");

            migrationBuilder.DropColumn(
                name: "SentenceStructureId",
                table: "FormStructureMappings");

            migrationBuilder.AlterColumn<string>(
                name: "SubCategoryId",
                table: "SubCategoryFormMappings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "SentenceFormId",
                table: "SubCategoryFormMappings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

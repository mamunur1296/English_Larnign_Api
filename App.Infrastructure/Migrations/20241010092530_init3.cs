using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormStructureMappings_SentenceFormss_SentenceFormId",
                table: "FormStructureMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_FormStructureMappings_SentenceStructures_SentenceStructureId",
                table: "FormStructureMappings");

            migrationBuilder.DropIndex(
                name: "IX_FormStructureMappings_SentenceFormId",
                table: "FormStructureMappings");

            migrationBuilder.DropColumn(
                name: "SentenceFormId",
                table: "FormStructureMappings");

            migrationBuilder.RenameColumn(
                name: "SentenceStructureId",
                table: "FormStructureMappings",
                newName: "SentenceStructureId1");

            migrationBuilder.RenameIndex(
                name: "IX_FormStructureMappings_SentenceStructureId",
                table: "FormStructureMappings",
                newName: "IX_FormStructureMappings_SentenceStructureId1");

            migrationBuilder.AlterColumn<string>(
                name: "StructureID",
                table: "FormStructureMappings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FormateID",
                table: "FormStructureMappings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_FormStructureMappings_FormateID",
                table: "FormStructureMappings",
                column: "FormateID");

            migrationBuilder.CreateIndex(
                name: "IX_FormStructureMappings_StructureID",
                table: "FormStructureMappings",
                column: "StructureID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormStructureMappings_SentenceFormss_FormateID",
                table: "FormStructureMappings",
                column: "FormateID",
                principalTable: "SentenceFormss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormStructureMappings_SentenceStructures_SentenceStructureId1",
                table: "FormStructureMappings",
                column: "SentenceStructureId1",
                principalTable: "SentenceStructures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormStructureMappings_SentenceStructures_StructureID",
                table: "FormStructureMappings",
                column: "StructureID",
                principalTable: "SentenceStructures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormStructureMappings_SentenceFormss_FormateID",
                table: "FormStructureMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_FormStructureMappings_SentenceStructures_SentenceStructureId1",
                table: "FormStructureMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_FormStructureMappings_SentenceStructures_StructureID",
                table: "FormStructureMappings");

            migrationBuilder.DropIndex(
                name: "IX_FormStructureMappings_FormateID",
                table: "FormStructureMappings");

            migrationBuilder.DropIndex(
                name: "IX_FormStructureMappings_StructureID",
                table: "FormStructureMappings");

            migrationBuilder.RenameColumn(
                name: "SentenceStructureId1",
                table: "FormStructureMappings",
                newName: "SentenceStructureId");

            migrationBuilder.RenameIndex(
                name: "IX_FormStructureMappings_SentenceStructureId1",
                table: "FormStructureMappings",
                newName: "IX_FormStructureMappings_SentenceStructureId");

            migrationBuilder.AlterColumn<string>(
                name: "StructureID",
                table: "FormStructureMappings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FormateID",
                table: "FormStructureMappings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SentenceFormId",
                table: "FormStructureMappings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormStructureMappings_SentenceFormId",
                table: "FormStructureMappings",
                column: "SentenceFormId");

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
        }
    }
}

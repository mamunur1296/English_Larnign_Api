using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    public partial class adsTableINclude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PublisherID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannerIDOnOff = table.Column<bool>(type: "bit", nullable: true),
                    InterstitialAdID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterstitialAdIDOnOff = table.Column<bool>(type: "bit", nullable: true),
                    NativAdID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NativAdIDOnOff = table.Column<bool>(type: "bit", nullable: true),
                    NativAdPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NativAdPositionOnOff = table.Column<bool>(type: "bit", nullable: true),
                    InterstitialClicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterstitialClicksOnOff = table.Column<bool>(type: "bit", nullable: true),
                    Rewardedinterstitial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardedinterstitialOnOff = table.Column<bool>(type: "bit", nullable: true),
                    RewardedAds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardedAdsOnOff = table.Column<bool>(type: "bit", nullable: true),
                    OpneAds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpneAdsOnOff = table.Column<bool>(type: "bit", nullable: true),
                    Tasting = table.Column<bool>(type: "bit", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adds");
        }
    }
}

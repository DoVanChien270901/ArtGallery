using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtGallery.Data.Migrations
{
    public partial class columnstatusinauc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Auctions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Auctions");
        }
    }
}

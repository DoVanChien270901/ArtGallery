using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtGallery.Data.Migrations
{
    public partial class updateauction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Auctions",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AccountId",
                table: "Auctions",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Accounts_AccountId",
                table: "Auctions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Accounts_AccountId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_AccountId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Auctions");
        }
    }
}

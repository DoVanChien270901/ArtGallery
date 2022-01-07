using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtGallery.Data.Migrations
{
    public partial class db72 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "AmountInActions",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmountInActions_AccountId",
                table: "AmountInActions",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AmountInActions_Accounts_AccountId",
                table: "AmountInActions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmountInActions_Accounts_AccountId",
                table: "AmountInActions");

            migrationBuilder.DropIndex(
                name: "IX_AmountInActions_AccountId",
                table: "AmountInActions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AmountInActions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameVault.REST.Migrations
{
    /// <inheritdoc />
    public partial class SteamGameEntityClusterization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Nickname_Email",
                table: "Users",
                columns: new[] { "Nickname", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SteamGames_SteamAppId",
                table: "SteamGames",
                column: "SteamAppId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Nickname_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_SteamGames_SteamAppId",
                table: "SteamGames");
        }
    }
}

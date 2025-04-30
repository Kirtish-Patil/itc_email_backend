using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddedDraftToEmailInteraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DraftInteractionId",
                table: "EmailInteractions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplyToInteractionId",
                table: "EmailInteractions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailInteractions_DraftInteractionId",
                table: "EmailInteractions",
                column: "DraftInteractionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailInteractions_EmailInteractions_DraftInteractionId",
                table: "EmailInteractions",
                column: "DraftInteractionId",
                principalTable: "EmailInteractions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailInteractions_EmailInteractions_DraftInteractionId",
                table: "EmailInteractions");

            migrationBuilder.DropIndex(
                name: "IX_EmailInteractions_DraftInteractionId",
                table: "EmailInteractions");

            migrationBuilder.DropColumn(
                name: "DraftInteractionId",
                table: "EmailInteractions");

            migrationBuilder.DropColumn(
                name: "ReplyToInteractionId",
                table: "EmailInteractions");
        }
    }
}

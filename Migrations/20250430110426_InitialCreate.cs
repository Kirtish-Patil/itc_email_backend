using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailApiService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowupFlag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedDateTime_DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDateTime_TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDateTime_DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDateTime_TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlagStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateTime_DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDateTime_TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowupFlag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailInteractionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GraphId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BccRecipients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body_ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body_Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyPreview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CcRecipients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConversationIndex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlagId = table.Column<int>(type: "int", nullable: true),
                    From_EmailAddress_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From_EmailAddress_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasAttachments = table.Column<bool>(type: "bit", nullable: false),
                    Importance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InferenceClassification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternetMessageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeliveryReceiptRequested = table.Column<bool>(type: "bit", nullable: true),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsReadReceiptRequested = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentFolderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReplyTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sender_EmailAddress_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sender_EmailAddress_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToRecipients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueBody_ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueBody_Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInteractionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailInteractionDetails_FollowupFlag_FlagId",
                        column: x => x.FlagId,
                        principalTable: "FollowupFlag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsInline = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    EmailInteractionDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_EmailInteractionDetails_EmailInteractionDetailsId",
                        column: x => x.EmailInteractionDetailsId,
                        principalTable: "EmailInteractionDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmailInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Queues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConversationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplyToInteractionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatestConvoResBodyPreview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<bool>(type: "bit", nullable: true),
                    EmailInteractionDetailsId = table.Column<int>(type: "int", nullable: false),
                    DraftInteractionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailInteractions_EmailInteractionDetails_EmailInteractionDetailsId",
                        column: x => x.EmailInteractionDetailsId,
                        principalTable: "EmailInteractionDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailInteractions_EmailInteractions_DraftInteractionId",
                        column: x => x.DraftInteractionId,
                        principalTable: "EmailInteractions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternetMessageHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailInteractionDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternetMessageHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternetMessageHeader_EmailInteractionDetails_EmailInteractionDetailsId",
                        column: x => x.EmailInteractionDetailsId,
                        principalTable: "EmailInteractionDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_EmailInteractionDetailsId",
                table: "Attachment",
                column: "EmailInteractionDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInteractionDetails_FlagId",
                table: "EmailInteractionDetails",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInteractions_DraftInteractionId",
                table: "EmailInteractions",
                column: "DraftInteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInteractions_EmailInteractionDetailsId",
                table: "EmailInteractions",
                column: "EmailInteractionDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternetMessageHeader_EmailInteractionDetailsId",
                table: "InternetMessageHeader",
                column: "EmailInteractionDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "EmailInteractions");

            migrationBuilder.DropTable(
                name: "InternetMessageHeader");

            migrationBuilder.DropTable(
                name: "EmailInteractionDetails");

            migrationBuilder.DropTable(
                name: "FollowupFlag");
        }
    }
}

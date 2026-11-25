using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestCoice_ContestWorks_WorkId",
                table: "ContestCoice");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestCoice_Users_UserId",
                table: "ContestCoice");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_RegistrationId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_UserId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestCoice",
                table: "ContestCoice");

            migrationBuilder.RenameTable(
                name: "ContestCoice",
                newName: "ContestChoices");

            migrationBuilder.RenameIndex(
                name: "IX_ContestCoice_WorkId",
                table: "ContestChoices",
                newName: "IX_ContestChoices_WorkId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestCoice_UserId",
                table: "ContestChoices",
                newName: "IX_ContestChoices_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Point",
                table: "UserAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "UserAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ContestWorks",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ContentURL",
                table: "ContestWorks",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Branches",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Branches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestChoices",
                table: "ContestChoices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EventBranchs",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventBranchs", x => new { x.EventId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_EventBranchs_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventBranchs_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_RegistrationId_QuestionId",
                table: "UserAnswers",
                columns: new[] { "RegistrationId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId_EventId",
                table: "Registrations",
                columns: new[] { "UserId", "EventId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EventBranchs_BranchId",
                table: "EventBranchs",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestChoices_ContestWorks_WorkId",
                table: "ContestChoices",
                column: "WorkId",
                principalTable: "ContestWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestChoices_Users_UserId",
                table: "ContestChoices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_CreatorId",
                table: "Events",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestChoices_ContestWorks_WorkId",
                table: "ContestChoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestChoices_Users_UserId",
                table: "ContestChoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_CreatorId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventBranchs");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_RegistrationId_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_UserId_EventId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Events_CreatorId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestChoices",
                table: "ContestChoices");

            migrationBuilder.RenameTable(
                name: "ContestChoices",
                newName: "ContestCoice");

            migrationBuilder.RenameIndex(
                name: "IX_ContestChoices_WorkId",
                table: "ContestCoice",
                newName: "IX_ContestCoice_WorkId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestChoices_UserId",
                table: "ContestCoice",
                newName: "IX_ContestCoice_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Point",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ContestWorks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "ContentURL",
                table: "ContestWorks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Branches",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestCoice",
                table: "ContestCoice",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_RegistrationId",
                table: "UserAnswers",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId",
                table: "Registrations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestCoice_ContestWorks_WorkId",
                table: "ContestCoice",
                column: "WorkId",
                principalTable: "ContestWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestCoice_Users_UserId",
                table: "ContestCoice",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

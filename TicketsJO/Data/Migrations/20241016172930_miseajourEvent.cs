using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsJO.Data.Migrations
{
    /// <inheritdoc />
    public partial class miseajourEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_StatutEvents_StatutId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "StatutId",
                table: "Events",
                newName: "StatutEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StatutId",
                table: "Events",
                newName: "IX_Events_StatutEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_StatutEvents_StatutEventId",
                table: "Events",
                column: "StatutEventId",
                principalTable: "StatutEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_StatutEvents_StatutEventId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "StatutEventId",
                table: "Events",
                newName: "StatutId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StatutEventId",
                table: "Events",
                newName: "IX_Events_StatutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_StatutEvents_StatutId",
                table: "Events",
                column: "StatutId",
                principalTable: "StatutEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsJO.Data.Migrations
{
    /// <inheritdoc />
    public partial class miseaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Offre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offre_EventId",
                table: "Offre",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Events_EventId",
                table: "Offre",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Events_EventId",
                table: "Offre");

            migrationBuilder.DropIndex(
                name: "IX_Offre_EventId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Offre");
        }
    }
}

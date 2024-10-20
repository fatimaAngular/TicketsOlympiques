using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsJO.Data.Migrations
{
    /// <inheritdoc />
    public partial class MisedeToutesLesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropTable(
                name: "TypeTickets");

            migrationBuilder.DropColumn(
                name: "TicketKey",
                table: "Tickets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketKey",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OffreID = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    IdOffre = table.Column<int>(type: "int", nullable: false),
                    IdTicket = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<int>(type: "int", nullable: false),
                    PrixUnitaire = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Offres_OffreID",
                        column: x => x.OffreID,
                        principalTable: "Offres",
                        principalColumn: "OffreID");
                    table.ForeignKey(
                        name: "FK_TicketDetails_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TypeTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTickets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_OffreID",
                table: "TicketDetails",
                column: "OffreID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails",
                column: "TicketId");
        }
    }
}

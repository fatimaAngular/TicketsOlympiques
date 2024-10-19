using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsJO.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreationDesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateNaissance",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Inscription",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ModeDePaiements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeDePaiements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatutEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatutTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutTickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paiements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePai = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Montant = table.Column<double>(type: "float", nullable: false),
                    ModeDePaieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paiements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paiements_ModeDePaiements_ModeDePaieId",
                        column: x => x.ModeDePaieId,
                        principalTable: "ModeDePaiements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEvent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Capacite = table.Column<int>(type: "int", nullable: false),
                    DisciplineID = table.Column<int>(type: "int", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdresseEvent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StatutEventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_Disciplines_DisciplineID",
                        column: x => x.DisciplineID,
                        principalTable: "Disciplines",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Events_StatutEvents_StatutEventId",
                        column: x => x.StatutEventId,
                        principalTable: "StatutEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Offres",
                columns: table => new
                {
                    OffreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateurId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offres", x => x.OffreID);
                    table.ForeignKey(
                        name: "FK_Offres_AspNetUsers_CreateurId",
                        column: x => x.CreateurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offres_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CartId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OffreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Carts_Offres_OffreID",
                        column: x => x.OffreID,
                        principalTable: "Offres",
                        principalColumn: "OffreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    DateTicket = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CleTicket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QrCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OffreIncludOffreID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Offres_OffreIncludOffreID",
                        column: x => x.OffreIncludOffreID,
                        principalTable: "Offres",
                        principalColumn: "OffreID");
                });

            migrationBuilder.CreateTable(
                name: "TicketDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTicket = table.Column<int>(type: "int", nullable: false),
                    IdOffre = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<int>(type: "int", nullable: false),
                    PrixUnitaire = table.Column<double>(type: "float", nullable: false),
                    OffreID = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ClientId",
                table: "Carts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_OffreID",
                table: "Carts",
                column: "OffreID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_DisciplineID",
                table: "Events",
                column: "DisciplineID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StatutEventId",
                table: "Events",
                column: "StatutEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Offres_CreateurId",
                table: "Offres",
                column: "CreateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Offres_EventId",
                table: "Offres",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_ModeDePaieId",
                table: "Paiements",
                column: "ModeDePaieId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_OffreID",
                table: "TicketDetails",
                column: "OffreID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClientId",
                table: "Tickets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OffreIncludOffreID",
                table: "Tickets",
                column: "OffreIncludOffreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Paiements");

            migrationBuilder.DropTable(
                name: "StatutTickets");

            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropTable(
                name: "TypeTickets");

            migrationBuilder.DropTable(
                name: "ModeDePaiements");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Offres");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "StatutEvents");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateNaissance",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Inscription",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "AspNetUsers");
        }
    }
}

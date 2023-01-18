using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApp.Migrations
{
    public partial class firstTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConferenceRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrOfSeatings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekDay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekNr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekDayBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    ConferenceRoomId = table.Column<int>(type: "int", nullable: false),
                    BookerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyBookerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Booked = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDayBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekDayBookings_ConferenceRooms_ConferenceRoomId",
                        column: x => x.ConferenceRoomId,
                        principalTable: "ConferenceRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeekDayBookings_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeekDayBookings_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeekDayBookings_ConferenceRoomId",
                table: "WeekDayBookings",
                column: "ConferenceRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekDayBookings_DayId",
                table: "WeekDayBookings",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekDayBookings_WeekId",
                table: "WeekDayBookings",
                column: "WeekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeekDayBookings");

            migrationBuilder.DropTable(
                name: "ConferenceRooms");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Weeks");
        }
    }
}

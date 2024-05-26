using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspDatabase.Migrations
{
    /// <inheritdoc />
    public partial class RequestNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberofPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passportSeries = table.Column<int>(type: "int", nullable: false),
                    nubmerPassport = table.Column<int>(type: "int", nullable: false),
                    BookingDateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingDateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idhotel = table.Column<int>(type: "int", nullable: true),
                    AdressRoom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRequests_Hotels_Idhotel",
                        column: x => x.Idhotel,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_Idhotel",
                table: "BookingRequests",
                column: "Idhotel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingRequests");
        }
    }
}

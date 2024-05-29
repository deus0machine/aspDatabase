using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspDatabase.Migrations
{
    /// <inheritdoc />
    public partial class NewBokingRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdressRoom",
                table: "BookingRequests");

            migrationBuilder.AddColumn<int>(
                name: "IdRoom",
                table: "BookingRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "BookingRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_RoomID",
                table: "BookingRequests",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Rooms_RoomID",
                table: "BookingRequests",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Rooms_RoomID",
                table: "BookingRequests");

            migrationBuilder.DropIndex(
                name: "IX_BookingRequests_RoomID",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "IdRoom",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "BookingRequests");

            migrationBuilder.AddColumn<string>(
                name: "AdressRoom",
                table: "BookingRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

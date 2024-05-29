using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspDatabase.Migrations
{
    /// <inheritdoc />
    public partial class BookingTrolling2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Hotels_Idhotel",
                table: "BookingRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Rooms_RoomID",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "IdRoom",
                table: "BookingRequests");

            migrationBuilder.RenameColumn(
                name: "RoomID",
                table: "BookingRequests",
                newName: "roomID");

            migrationBuilder.RenameColumn(
                name: "Idhotel",
                table: "BookingRequests",
                newName: "hotelId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingRequests_RoomID",
                table: "BookingRequests",
                newName: "IX_BookingRequests_roomID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingRequests_Idhotel",
                table: "BookingRequests",
                newName: "IX_BookingRequests_hotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Hotels_hotelId",
                table: "BookingRequests",
                column: "hotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Rooms_roomID",
                table: "BookingRequests",
                column: "roomID",
                principalTable: "Rooms",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Hotels_hotelId",
                table: "BookingRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Rooms_roomID",
                table: "BookingRequests");

            migrationBuilder.RenameColumn(
                name: "roomID",
                table: "BookingRequests",
                newName: "RoomID");

            migrationBuilder.RenameColumn(
                name: "hotelId",
                table: "BookingRequests",
                newName: "Idhotel");

            migrationBuilder.RenameIndex(
                name: "IX_BookingRequests_roomID",
                table: "BookingRequests",
                newName: "IX_BookingRequests_RoomID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingRequests_hotelId",
                table: "BookingRequests",
                newName: "IX_BookingRequests_Idhotel");

            migrationBuilder.AddColumn<int>(
                name: "IdRoom",
                table: "BookingRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Hotels_Idhotel",
                table: "BookingRequests",
                column: "Idhotel",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Rooms_RoomID",
                table: "BookingRequests",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID");
        }
    }
}

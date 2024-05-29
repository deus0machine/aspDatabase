using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_Idhotel",
                table: "Rooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_Idhotel",
                table: "Rooms",
                column: "Idhotel",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_Idhotel",
                table: "Rooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_Idhotel",
                table: "Rooms",
                column: "Idhotel",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}

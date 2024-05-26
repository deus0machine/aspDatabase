using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AgreementUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "hotelId",
                table: "Agreements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "clientID",
                table: "Agreements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "cost",
                table: "Agreements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_clientID",
                table: "Agreements",
                column: "clientID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_hotelId",
                table: "Agreements",
                column: "hotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_roomID",
                table: "Agreements",
                column: "roomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_Clients_clientID",
                table: "Agreements",
                column: "clientID",
                principalTable: "Clients",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_Hotels_hotelId",
                table: "Agreements",
                column: "hotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_Rooms_roomID",
                table: "Agreements",
                column: "roomID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_Clients_clientID",
                table: "Agreements");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_Hotels_hotelId",
                table: "Agreements");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_Rooms_roomID",
                table: "Agreements");

            migrationBuilder.DropIndex(
                name: "IX_Agreements_clientID",
                table: "Agreements");

            migrationBuilder.DropIndex(
                name: "IX_Agreements_hotelId",
                table: "Agreements");

            migrationBuilder.DropIndex(
                name: "IX_Agreements_roomID",
                table: "Agreements");

            migrationBuilder.DropColumn(
                name: "cost",
                table: "Agreements");

            migrationBuilder.AlterColumn<int>(
                name: "hotelId",
                table: "Agreements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "clientID",
                table: "Agreements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

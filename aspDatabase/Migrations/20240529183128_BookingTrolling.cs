using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspDatabase.Migrations
{
    /// <inheritdoc />
    public partial class BookingTrolling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_Clients_clientID",
                table: "Agreements");

            migrationBuilder.AlterColumn<int>(
                name: "clientID",
                table: "Agreements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_Clients_clientID",
                table: "Agreements",
                column: "clientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_Clients_clientID",
                table: "Agreements");

            migrationBuilder.AlterColumn<int>(
                name: "clientID",
                table: "Agreements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_Clients_clientID",
                table: "Agreements",
                column: "clientID",
                principalTable: "Clients",
                principalColumn: "ID");
        }
    }
}

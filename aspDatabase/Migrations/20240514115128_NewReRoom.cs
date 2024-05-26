using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspDatabase.Migrations
{
    /// <inheritdoc />
    public partial class NewReRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Idhotel",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Idhotel",
                table: "Rooms",
                column: "Idhotel");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_Idhotel",
                table: "Rooms",
                column: "Idhotel",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_Idhotel",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_Idhotel",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "Idhotel",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

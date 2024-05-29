using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspDatabase.Migrations
{
    /// <inheritdoc />
    public partial class funcCostAgreement2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cost",
                table: "Agreements",
                newName: "Cost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Agreements",
                newName: "cost");
        }
    }
}

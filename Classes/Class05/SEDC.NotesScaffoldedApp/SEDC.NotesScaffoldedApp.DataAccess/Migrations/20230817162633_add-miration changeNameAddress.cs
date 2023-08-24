using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.NotesScaffoldedApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addmirationchangeNameAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Users",
                newName: "HomeAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomeAddress",
                table: "Users",
                newName: "Address");
        }
    }
}

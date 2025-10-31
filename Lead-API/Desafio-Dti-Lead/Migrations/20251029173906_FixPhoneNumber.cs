using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Dti_Lead.Migrations
{
    /// <inheritdoc />
    public partial class FixPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "leads",
                newName: "FristName");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "leads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FristName",
                table: "leads",
                newName: "FirstName");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "leads",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

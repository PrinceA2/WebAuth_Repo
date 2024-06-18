using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAuth.Migrations
{
    /// <inheritdoc />
    public partial class InitCat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserMessage",
                table: "TblUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "default_value",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserMessage",
                table: "TblUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "default_value");
        }
    }
}

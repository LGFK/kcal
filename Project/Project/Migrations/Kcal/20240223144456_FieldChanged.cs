using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations.Kcal
{
    /// <inheritdoc />
    public partial class FieldChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "UserId",
            table: "DailyRatio",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations.Kcal
{
    /// <inheritdoc />
    public partial class AnotherTypeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
        name: "UserId",
        table: "DailyRatio");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DailyRatio",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

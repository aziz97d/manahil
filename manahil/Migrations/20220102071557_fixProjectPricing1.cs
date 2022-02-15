using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class fixProjectPricing1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectCategoryId",
                table: "ProjectPricings");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProjectPricings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProjectPricings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProjectCategoryId",
                table: "ProjectPricings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

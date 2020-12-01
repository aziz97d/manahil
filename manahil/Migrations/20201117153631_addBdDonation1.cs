using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class addBdDonation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategroyId",
                table: "BdDonations");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "BdDonations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "BdDonations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CategroyId",
                table: "BdDonations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

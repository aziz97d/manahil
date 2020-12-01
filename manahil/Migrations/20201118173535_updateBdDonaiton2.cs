using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class updateBdDonaiton2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceivedNumber",
                table: "BdDonations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedNumber",
                table: "BdDonations");
        }
    }
}

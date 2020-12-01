using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class addBdDonation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BdDonations",
                columns: table => new
                {
                    BdDonationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategroyId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PaymnetType = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BdDonations", x => x.BdDonationId);
                    table.ForeignKey(
                        name: "FK_BdDonations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BdDonations_CategoryId",
                table: "BdDonations",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BdDonations");
        }
    }
}

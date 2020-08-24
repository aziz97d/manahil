using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class addMngDrive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManageDrives",
                columns: table => new
                {
                    ManageDriveId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    DriveLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManageDrives", x => x.ManageDriveId);
                    table.ForeignKey(
                        name: "FK_ManageDrives_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManageDrives_DonorId",
                table: "ManageDrives",
                column: "DonorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManageDrives");
        }
    }
}

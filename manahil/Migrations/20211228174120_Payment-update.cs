using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class Paymentupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Donors_DonorId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_DonorId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaidProjects",
                table: "Payments");

            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "Payments",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalAmount",
                table: "Payments",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    PaymentDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.PaymentDetailsId);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPricings",
                columns: table => new
                {
                    ProectPricingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorId = table.Column<int>(nullable: false),
                    ProjectCategoryId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    ContractorId = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPricings", x => x.ProectPricingId);
                    table.ForeignKey(
                        name: "FK_ProjectPricings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPricings_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "ContractorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPricings_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_PaymentId",
                table: "PaymentDetails",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_ProjectId",
                table: "PaymentDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPricings_CategoryId",
                table: "ProjectPricings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPricings_ContractorId",
                table: "ProjectPricings",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPricings_DonorId",
                table: "ProjectPricings",
                column: "DonorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "ProjectPricings");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "DonorId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaidProjects",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DonorId",
                table: "Payments",
                column: "DonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Donors_DonorId",
                table: "Payments",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "DonorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class addApprovalProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalProjects",
                columns: table => new
                {
                    ApprovalProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNo = table.Column<int>(nullable: false),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectImplementAddress = table.Column<string>(nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndtDate = table.Column<DateTime>(nullable: true),
                    DonorId = table.Column<int>(nullable: true),
                    AuditStatus = table.Column<string>(nullable: true),
                    CertificateStatus = table.Column<string>(nullable: true),
                    ApprovedMoney = table.Column<double>(nullable: false),
                    CurrencyRate = table.Column<double>(nullable: false),
                    TotalAcceptedMoney = table.Column<double>(nullable: false),
                    ExpenseAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalProjects", x => x.ApprovalProjectId);
                    table.ForeignKey(
                        name: "FK_ApprovalProjects_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalProjects_DonorId",
                table: "ApprovalProjects",
                column: "DonorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalProjects");
        }
    }
}

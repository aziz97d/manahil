using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class addDeposit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepositAccounts",
                columns: table => new
                {
                    DepositAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCode = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    DepositType = table.Column<string>(nullable: true),
                    DepositAmount = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositAccounts", x => x.DepositAccountId);
                });

            migrationBuilder.CreateTable(
                name: "TransferAccounts",
                columns: table => new
                {
                    TransferAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositAccountId = table.Column<int>(nullable: false),
                    TransferDate = table.Column<DateTime>(nullable: false),
                    TransferAmount = table.Column<double>(nullable: false),
                    ContractorName = table.Column<string>(nullable: true),
                    AgainstProject = table.Column<string>(nullable: true),
                    FC1orFD7 = table.Column<string>(nullable: true),
                    ApprovalStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferAccounts", x => x.TransferAccountId);
                    table.ForeignKey(
                        name: "FK_TransferAccounts_DepositAccounts_DepositAccountId",
                        column: x => x.DepositAccountId,
                        principalTable: "DepositAccounts",
                        principalColumn: "DepositAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferAccounts_DepositAccountId",
                table: "TransferAccounts",
                column: "DepositAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferAccounts");

            migrationBuilder.DropTable(
                name: "DepositAccounts");
        }
    }
}

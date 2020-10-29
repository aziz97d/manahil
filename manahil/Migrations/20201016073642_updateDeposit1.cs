using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class updateDeposit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionCode",
                table: "DepositAccounts");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "DepositAccounts");

            migrationBuilder.AddColumn<string>(
                name: "DepositCode",
                table: "DepositAccounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepositDate",
                table: "DepositAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositCode",
                table: "DepositAccounts");

            migrationBuilder.DropColumn(
                name: "DepositDate",
                table: "DepositAccounts");

            migrationBuilder.AddColumn<string>(
                name: "TransactionCode",
                table: "DepositAccounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "DepositAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

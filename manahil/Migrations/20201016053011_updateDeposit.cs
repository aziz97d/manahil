using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class updateDeposit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "DepositAccounts",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Donor",
                table: "DepositAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Donor",
                table: "DepositAccounts");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "DepositAccounts",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}

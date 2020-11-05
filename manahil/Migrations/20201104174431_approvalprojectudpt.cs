using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class approvalprojectudpt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalAcceptedMoney",
                table: "ApprovalProjects",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalAcceptedMoney",
                table: "ApprovalProjects",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

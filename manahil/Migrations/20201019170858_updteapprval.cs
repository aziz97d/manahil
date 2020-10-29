using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class updteapprval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalProjects_Donors_DonorId",
                table: "ApprovalProjects");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalProjects_DonorId",
                table: "ApprovalProjects");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "ApprovalProjects");

            migrationBuilder.AlterColumn<string>(
                name: "ContractorName",
                table: "TransferAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgainstProject",
                table: "TransferAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Donor",
                table: "ApprovalProjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Donor",
                table: "ApprovalProjects");

            migrationBuilder.AlterColumn<string>(
                name: "ContractorName",
                table: "TransferAccounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AgainstProject",
                table: "TransferAccounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "DonorId",
                table: "ApprovalProjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalProjects_DonorId",
                table: "ApprovalProjects",
                column: "DonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalProjects_Donors_DonorId",
                table: "ApprovalProjects",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "DonorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

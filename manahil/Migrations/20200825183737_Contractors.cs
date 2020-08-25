using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class Contractors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldWorkers_Cities_CityId",
                table: "FieldWorkers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_FieldWorkers_ContractorId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_FieldWorkers_ContractorId1",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldWorkers",
                table: "FieldWorkers");

            migrationBuilder.RenameTable(
                name: "FieldWorkers",
                newName: "Contractors");

            migrationBuilder.RenameIndex(
                name: "IX_FieldWorkers_CityId",
                table: "Contractors",
                newName: "IX_Contractors_CityId");

            migrationBuilder.AddColumn<int>(
                name: "ManahilSerial",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_Cities_CityId",
                table: "Contractors",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contractors_ContractorId",
                table: "Payments",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Contractors_ContractorId1",
                table: "Projects",
                column: "ContractorId1",
                principalTable: "Contractors",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_Cities_CityId",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contractors_ContractorId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Contractors_ContractorId1",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "ManahilSerial",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Contractors",
                newName: "FieldWorkers");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_CityId",
                table: "FieldWorkers",
                newName: "IX_FieldWorkers_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldWorkers",
                table: "FieldWorkers",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldWorkers_Cities_CityId",
                table: "FieldWorkers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_FieldWorkers_ContractorId",
                table: "Payments",
                column: "ContractorId",
                principalTable: "FieldWorkers",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_FieldWorkers_ContractorId1",
                table: "Projects",
                column: "ContractorId1",
                principalTable: "FieldWorkers",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

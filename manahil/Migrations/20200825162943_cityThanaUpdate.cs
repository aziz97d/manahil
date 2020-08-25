using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class cityThanaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Thanas_ThanaId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldWorkers_Thanas_ThanaId",
                table: "FieldWorkers");

            migrationBuilder.DropIndex(
                name: "IX_FieldWorkers_ThanaId",
                table: "FieldWorkers");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ThanaId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ThanaId",
                table: "FieldWorkers");

            migrationBuilder.DropColumn(
                name: "ThanaId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "FieldWorkers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkers_CityId",
                table: "FieldWorkers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CityId",
                table: "Employees",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Cities_CityId",
                table: "Employees",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldWorkers_Cities_CityId",
                table: "FieldWorkers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Cities_CityId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldWorkers_Cities_CityId",
                table: "FieldWorkers");

            migrationBuilder.DropIndex(
                name: "IX_FieldWorkers_CityId",
                table: "FieldWorkers");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CityId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "FieldWorkers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "ThanaId",
                table: "FieldWorkers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThanaId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkers_ThanaId",
                table: "FieldWorkers",
                column: "ThanaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ThanaId",
                table: "Employees",
                column: "ThanaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Thanas_ThanaId",
                table: "Employees",
                column: "ThanaId",
                principalTable: "Thanas",
                principalColumn: "ThanaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldWorkers_Thanas_ThanaId",
                table: "FieldWorkers",
                column: "ThanaId",
                principalTable: "Thanas",
                principalColumn: "ThanaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

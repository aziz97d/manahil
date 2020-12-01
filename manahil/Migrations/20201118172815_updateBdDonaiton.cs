using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class updateBdDonaiton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymnetType",
                table: "BdDonations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BdDonations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                table: "BdDonations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BdDonations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "BdDonations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TranxCode",
                table: "BdDonations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BdDonations_PaymentTypeId",
                table: "BdDonations",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BdDonations_PaymentTypes_PaymentTypeId",
                table: "BdDonations",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BdDonations_PaymentTypes_PaymentTypeId",
                table: "BdDonations");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_BdDonations_PaymentTypeId",
                table: "BdDonations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BdDonations");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "BdDonations");

            migrationBuilder.DropColumn(
                name: "TranxCode",
                table: "BdDonations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BdDonations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                table: "BdDonations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "PaymnetType",
                table: "BdDonations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

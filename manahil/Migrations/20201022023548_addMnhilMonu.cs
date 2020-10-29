using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace manahil.Migrations
{
    public partial class addMnhilMonu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManahilMonumentals",
                columns: table => new
                {
                    ManahilMonumentalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    MonumentalNo = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManahilMonumentals", x => x.ManahilMonumentalId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManahilMonumentals");
        }
    }
}

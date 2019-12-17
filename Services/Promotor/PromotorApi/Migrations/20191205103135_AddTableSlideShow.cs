using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromotorApi.Migrations
{
    public partial class AddTableSlideShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotorApi_SlideShow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false, defaultValue: ""),
                    Description = table.Column<string>(nullable: true, defaultValue: ""),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    ValidFrom = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 12, 5, 11, 31, 35, 463, DateTimeKind.Local).AddTicks(8907)),
                    ValidTo = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotorApi_SlideShow", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotorApi_SlideShow");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromotorApi.Migrations
{
    public partial class AddTableSlideShowItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFrom",
                table: "PromotorApi_SlideShow",
                nullable: false,
                defaultValue: new DateTime(2019, 12, 5, 11, 42, 13, 307, DateTimeKind.Local).AddTicks(6930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2019, 12, 5, 11, 31, 35, 463, DateTimeKind.Local).AddTicks(8907));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "PromotorApi_SlideShow",
                nullable: false,
                defaultValue: new DateTime(2019, 12, 5, 11, 42, 13, 311, DateTimeKind.Local).AddTicks(9415));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "PromotorApi_SlideShow",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PromotorApi_SlideShowItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true, defaultValue: ""),
                    Description = table.Column<string>(nullable: true, defaultValue: ""),
                    SlideShowId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: false, defaultValue: "<div><h1>Proszę uzupełnić treść slajdu</h1></div>")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotorApi_SlideShowItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotorApi_SlideShowItem_PromotorApi_SlideShow_SlideShowId",
                        column: x => x.SlideShowId,
                        principalTable: "PromotorApi_SlideShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotorApi_SlideShowItem_SlideShowId",
                table: "PromotorApi_SlideShowItem",
                column: "SlideShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotorApi_SlideShowItem");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "PromotorApi_SlideShow");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "PromotorApi_SlideShow");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFrom",
                table: "PromotorApi_SlideShow",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2019, 12, 5, 11, 31, 35, 463, DateTimeKind.Local).AddTicks(8907),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 12, 5, 11, 42, 13, 307, DateTimeKind.Local).AddTicks(6930));
        }
    }
}

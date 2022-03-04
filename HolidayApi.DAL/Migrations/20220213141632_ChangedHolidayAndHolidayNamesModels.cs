using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolidayApi.DAL.Migrations
{
    public partial class ChangedHolidayAndHolidayNamesModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayNames_Holidays_HolidayId",
                table: "HolidayNames");

            migrationBuilder.DropIndex(
                name: "IX_HolidayNames_HolidayId",
                table: "HolidayNames");

            migrationBuilder.DropColumn(
                name: "HolidayId",
                table: "HolidayNames");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HolidayId",
                table: "HolidayNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HolidayNames_HolidayId",
                table: "HolidayNames",
                column: "HolidayId");

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayNames_Holidays_HolidayId",
                table: "HolidayNames",
                column: "HolidayId",
                principalTable: "Holidays",
                principalColumn: "HolidayId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

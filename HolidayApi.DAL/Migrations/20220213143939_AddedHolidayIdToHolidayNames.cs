using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolidayApi.DAL.Migrations
{
    public partial class AddedHolidayIdToHolidayNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HolidayId",
                table: "HolidayNames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolidayId",
                table: "HolidayNames");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolidayApi.DAL.Migrations
{
    public partial class ChangedFromStringToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaximumFreeDaysResult",
                table: "MaximumFreeDays",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaximumFreeDaysResult",
                table: "MaximumFreeDays",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

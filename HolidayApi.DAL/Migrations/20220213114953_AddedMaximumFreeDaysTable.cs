using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolidayApi.DAL.Migrations
{
    public partial class AddedMaximumFreeDaysTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaximumFreeDays",
                columns: table => new
                {
                    MaximumFreeDaysId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    MaximumFreeDaysResult = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaximumFreeDays", x => x.MaximumFreeDaysId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaximumFreeDays");
        }
    }
}

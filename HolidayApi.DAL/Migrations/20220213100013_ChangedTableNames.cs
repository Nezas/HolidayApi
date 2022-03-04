using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolidayApi.DAL.Migrations
{
    public partial class ChangedTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayType_Countries_CountryCode",
                table: "HolidayType");

            migrationBuilder.DropForeignKey(
                name: "FK_Region_Countries_CountryCode",
                table: "Region");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayType",
                table: "HolidayType");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "HolidayType",
                newName: "HolidayTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Region_CountryCode",
                table: "Regions",
                newName: "IX_Regions_CountryCode");

            migrationBuilder.RenameIndex(
                name: "IX_HolidayType_CountryCode",
                table: "HolidayTypes",
                newName: "IX_HolidayTypes_CountryCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayTypes",
                table: "HolidayTypes",
                column: "HolidayTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayTypes_Countries_CountryCode",
                table: "HolidayTypes",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "CountryCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryCode",
                table: "Regions",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "CountryCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayTypes_Countries_CountryCode",
                table: "HolidayTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryCode",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidayTypes",
                table: "HolidayTypes");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.RenameTable(
                name: "HolidayTypes",
                newName: "HolidayType");

            migrationBuilder.RenameIndex(
                name: "IX_Regions_CountryCode",
                table: "Region",
                newName: "IX_Region_CountryCode");

            migrationBuilder.RenameIndex(
                name: "IX_HolidayTypes_CountryCode",
                table: "HolidayType",
                newName: "IX_HolidayType_CountryCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidayType",
                table: "HolidayType",
                column: "HolidayTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayType_Countries_CountryCode",
                table: "HolidayType",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "CountryCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Region_Countries_CountryCode",
                table: "Region",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "CountryCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

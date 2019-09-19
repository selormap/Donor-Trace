using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorTraceAPI.Migrations
{
    public partial class UpdateOfficer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Officers_Facilities_FacilityId",
                table: "Officers");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityId",
                table: "Officers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Officers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Officers_Facilities_FacilityId",
                table: "Officers",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Officers_Facilities_FacilityId",
                table: "Officers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Officers");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityId",
                table: "Officers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Officers_Facilities_FacilityId",
                table: "Officers",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorTraceAPI.Migrations
{
    public partial class BloodTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlooGroupId",
                table: "BloodTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlooGroupId",
                table: "BloodTypes",
                nullable: false,
                defaultValue: 0);
        }
    }
}

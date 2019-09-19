using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorTraceAPI.Migrations
{
    public partial class UpdateDonorOrgan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorOrgans_BloodTypes_BloodTypeId",
                table: "DonorOrgans");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorOrgans_OrganLists_OrganListId",
                table: "DonorOrgans");

            migrationBuilder.DropIndex(
                name: "IX_DonorOrgans_BloodTypeId",
                table: "DonorOrgans");

            migrationBuilder.DropIndex(
                name: "IX_DonorOrgans_OrganListId",
                table: "DonorOrgans");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DonorOrgans",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DonorOrgans",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.CreateIndex(
                name: "IX_DonorOrgans_BloodTypeId",
                table: "DonorOrgans",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorOrgans_OrganListId",
                table: "DonorOrgans",
                column: "OrganListId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorOrgans_BloodTypes_BloodTypeId",
                table: "DonorOrgans",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DonorOrgans_OrganLists_OrganListId",
                table: "DonorOrgans",
                column: "OrganListId",
                principalTable: "OrganLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

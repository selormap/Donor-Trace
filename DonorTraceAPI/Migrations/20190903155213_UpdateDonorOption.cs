using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorTraceAPI.Migrations
{
    public partial class UpdateDonorOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorOrgans_BloodTypes_BloodTypeId",
                table: "DonorOrgans");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorOrgans_OrganLists_OrganListId",
                table: "DonorOrgans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonorOrgans",
                table: "DonorOrgans");

            migrationBuilder.AlterColumn<int>(
                name: "OrganListId",
                table: "DonorOrgans",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BloodTypeId",
                table: "DonorOrgans",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DonorOrgans",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DonorOrgans",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonorOrgans",
                table: "DonorOrgans",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorOrgans_BloodTypes_BloodTypeId",
                table: "DonorOrgans");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorOrgans_OrganLists_OrganListId",
                table: "DonorOrgans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonorOrgans",
                table: "DonorOrgans");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DonorOrgans");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DonorOrgans",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganListId",
                table: "DonorOrgans",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BloodTypeId",
                table: "DonorOrgans",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonorOrgans",
                table: "DonorOrgans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorOrgans_BloodTypes_BloodTypeId",
                table: "DonorOrgans",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonorOrgans_OrganLists_OrganListId",
                table: "DonorOrgans",
                column: "OrganListId",
                principalTable: "OrganLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

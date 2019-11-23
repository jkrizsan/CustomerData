using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyData.Data.Migrations
{
    public partial class AddCompanyIdtoContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Companys_CompanyId",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Companys_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Companys_CompanyId",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Contacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Companys_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

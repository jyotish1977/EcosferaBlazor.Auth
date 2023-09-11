using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CS8981
namespace EcosferaBlazor.Auth.Migrators.MSSQL.Migrations
{
    public partial class rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Site",
                table: "AspNetUsers",
                newName: "Provider");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Provider",
                table: "AspNetUsers",
                newName: "Site");
        }
    }
}

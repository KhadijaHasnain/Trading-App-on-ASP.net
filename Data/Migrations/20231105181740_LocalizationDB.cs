using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIGQXWebsite.Data.Migrations
{
  public partial class LocalizationDB : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Resource",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false),
            Language = table.Column<byte>(type: "tinyint", nullable: false),
            Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Position = table.Column<byte>(type: "tinyint", nullable: false),
            Type = table.Column<byte>(type: "tinyint", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Resource", x => new { x.Id, x.Language });
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Resource");
    }
  }
}

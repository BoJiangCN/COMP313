using Microsoft.EntityFrameworkCore.Migrations;

namespace WeLinkUp.Migrations
{
    public partial class removefreetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Freetime");

            migrationBuilder.DropColumn(
                name: "Freetime",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Freetime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Freetime",
                columns: table => new
                {
                    FreeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Days = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freetime", x => x.FreeID);
                });
        }
    }
}

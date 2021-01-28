using Microsoft.EntityFrameworkCore.Migrations;

namespace DSU21_5.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    ArtworkId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ArtName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.ArtworkId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artworks");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}

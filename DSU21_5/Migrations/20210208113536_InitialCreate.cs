using Microsoft.EntityFrameworkCore.Migrations;

namespace DSU21_5.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<string>(nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Exhibit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    MemberId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exhibit_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Members_UserId",
                        column: x => x.UserId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    ArtworkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ArtName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ExhibitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.ArtworkId);
                    table.ForeignKey(
                        name: "FK_Artworks_Exhibit_ExhibitId",
                        column: x => x.ExhibitId,
                        principalTable: "Exhibit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Artworks_Members_UserId",
                        column: x => x.UserId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_ExhibitId",
                table: "Artworks",
                column: "ExhibitId");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_UserId",
                table: "Artworks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exhibit_MemberId",
                table: "Exhibit",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artworks");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Exhibit");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}

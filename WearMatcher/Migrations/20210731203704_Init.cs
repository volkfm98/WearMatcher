using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WearMatcher.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClothingItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ImgPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchingItemsPair",
                columns: table => new
                {
                    FirstItemId = table.Column<int>(type: "integer", nullable: false),
                    SecondItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchingItemsPair", x => new { x.FirstItemId, x.SecondItemId });
                    table.ForeignKey(
                        name: "FK_MatchingItemsPair_ClothingItem_FirstItemId",
                        column: x => x.FirstItemId,
                        principalTable: "ClothingItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchingItemsPair_ClothingItem_SecondItemId",
                        column: x => x.SecondItemId,
                        principalTable: "ClothingItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothingItemTag",
                columns: table => new
                {
                    ClothingItemsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingItemTag", x => new { x.ClothingItemsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ClothingItemTag_ClothingItem_ClothingItemsId",
                        column: x => x.ClothingItemsId,
                        principalTable: "ClothingItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothingItemTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingItemTag_TagsId",
                table: "ClothingItemTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingItemsPair_SecondItemId",
                table: "MatchingItemsPair",
                column: "SecondItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingItemTag");

            migrationBuilder.DropTable(
                name: "MatchingItemsPair");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "ClothingItem");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCollections.Migrations
{
    public partial class SeededDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ProductNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, null, "PORSCHE" },
                    { 2, null, "McLAREN" },
                    { 3, null, "HONDA" },
                    { 4, null, "TYOTA" },
                    { 5, null, "NISSAN" },
                    { 6, null, "MAZDA" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "ImagePath", "Name", "ProductNo", "Year" },
                values: new object[,]
                {
                    { 5, 1, "/images/96 porsche carrera.jpg", "96 PORSCHE CARRERA", "HTF01", 2025 },
                    { 1, 2, "/images/mclaren f1.jpg", "McLAREN F1", "HTB11", 2025 },
                    { 2, 3, "/images/honda s800 racing.jpg", "HONDA S800 RACING", "HRY58", 2025 },
                    { 3, 4, "/images/94 toyota supra.jpg", "94 TYOTA SUPRA", "HTF27", 2025 },
                    { 7, 5, "/images/nissan skyline gt-r.jpg", "NISSAN SKYLINE GT-R", "HYY72", 2025 },
                    { 4, 6, "/images/95 mazda rx-7.jpg", "95 MAZDA RX-7", "HTD97", 2025 },
                    { 6, 6, "/images/91 mazda mx-5 miata.jpg", "91 MAZDA MX-5 MIATA", "HTD80", 2025 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}

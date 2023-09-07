using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sample.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Id", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImgUri", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, "https://rohlik.img", "Rohlík", 3.5m },
                    { 2, null, "https://houska.img", "Houska", 3.5m },
                    { 3, "houskový knedlík", "https://knedlik.img", "Knedlík", 24.9m },
                    { 4, null, "https://vanocka.img", "Vánočka", 49.9m },
                    { 5, null, "https://pomeranc.img", "Pomeranč", 5m },
                    { 6, null, "https://kiwi.img", "Kiwi", 7m },
                    { 7, null, "https://syr.img", "Sýr", 18.9m },
                    { 8, null, "https://jogurt.img", "Jogurt", 25.9m },
                    { 9, null, "https://salam.img", "Salám", 69.9m },
                    { 10, null, "https://mleko.img", "Mléko", 14.9m },
                    { 11, null, "https://paprika.img", "Paprika", 20m },
                    { 12, null, "https://patizon.img", "Patizon", 22m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

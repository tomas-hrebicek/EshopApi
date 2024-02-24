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
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ImgUri = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Id", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImgUri", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, "https://rohlik.img/", "Rohlík", 3.5m },
                    { 2, null, "https://houska.img/", "Houska", 3.5m },
                    { 3, "houskový knedlík", "https://knedlik.img/", "Knedlík", 24.9m },
                    { 4, null, "https://vanocka.img/", "Vánočka", 49.9m },
                    { 5, null, "https://pomeranc.img/", "Pomeranč", 5m },
                    { 6, null, "https://kiwi.img/", "Kiwi", 7m },
                    { 7, null, "https://syr.img/", "Sýr", 18.9m },
                    { 8, null, "https://jogurt.img/", "Jogurt", 25.9m },
                    { 9, null, "https://salam.img/", "Salám", 69.9m },
                    { 10, null, "https://mleko.img/", "Mléko", 14.9m },
                    { 11, null, "https://paprika.img/", "Paprika", 20m },
                    { 12, null, "https://patizon.img/", "Patizon", 22m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Password", "Roles", "Salt", "Username" },
                values: new object[] { 1, true, "53C15AEBC31C921D5CEB4DF7E15E279C06606855F0D8BF8542A5D5CDEA71D74BA741BA116F8BAB4C6F40AA3076000027747522EE268B572E8411F85ABC7AF711", "[1]", "5D03756620AA910B167E97F8232967656A0DF57D36141C98B571F4059CEB8AB669288B3D10B8A0D49FF35C931477D02CEC89685ABB797918831E45F98A7A0DC2", "test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

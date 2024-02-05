using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Id", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Password", "Salt", "Username" },
                values: new object[] { 1, true, "53C15AEBC31C921D5CEB4DF7E15E279C06606855F0D8BF8542A5D5CDEA71D74BA741BA116F8BAB4C6F40AA3076000027747522EE268B572E8411F85ABC7AF711", "5D03756620AA910B167E97F8232967656A0DF57D36141C98B571F4059CEB8AB669288B3D10B8A0D49FF35C931477D02CEC89685ABB797918831E45F98A7A0DC2", "test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

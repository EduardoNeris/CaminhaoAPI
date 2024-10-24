using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaminhaoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caminhoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<int>(type: "int", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    CodigoChassi = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Planta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhoes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caminhoes");
        }
    }
}

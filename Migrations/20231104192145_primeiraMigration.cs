using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppTurismoAPI.Migrations
{
    /// <inheritdoc />
    public partial class primeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PontoTuristicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoTuristicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontoTuristicos_Cidades_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cidades",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "O Coração financeiro do Brasil.", "São Paulo" },
                    { 2, "O cartão postal do Brasil.", "Rio de Janeiro" },
                    { 3, "A cidade do carnaval.", "Recife" }
                });

            migrationBuilder.InsertData(
                table: "PontoTuristicos",
                columns: new[] { "Id", "CidadeId", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, 1, "A maior feira livre do mundo.", "Feira livre" },
                    { 2, 2, "A estátua mais famosa do mundo.", "Cristo redentor" },
                    { 3, 3, "A cidade do carnaval.", "Praia de boa viagem" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PontoTuristicos_CidadeId",
                table: "PontoTuristicos",
                column: "CidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontoTuristicos");

            migrationBuilder.DropTable(
                name: "Cidades");
        }
    }
}

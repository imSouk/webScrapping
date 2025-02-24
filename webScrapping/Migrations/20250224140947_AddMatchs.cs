using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webScrapping.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matchs",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkP1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkP2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkMatch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetScoreP1 = table.Column<int>(type: "int", nullable: false),
                    SetScoreP2 = table.Column<int>(type: "int", nullable: false),
                    P1Id = table.Column<int>(type: "int", nullable: false),
                    P2Id = table.Column<int>(type: "int", nullable: false),
                    P1Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P2Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchs", x => x.MatchId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matchs");
        }
    }
}

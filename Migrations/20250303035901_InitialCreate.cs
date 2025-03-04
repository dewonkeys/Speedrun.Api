using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Speedrun.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SRSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SRSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SRStrain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SRSystemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SRStrain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SRStrain_SRSystem_SRSystemId",
                        column: x => x.SRSystemId,
                        principalTable: "SRSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SRSegment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MsDuration = table.Column<int>(type: "integer", nullable: false),
                    SRStrainId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SRSegment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SRSegment_SRStrain_SRStrainId",
                        column: x => x.SRStrainId,
                        principalTable: "SRStrain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SRSegment_SRStrainId",
                table: "SRSegment",
                column: "SRStrainId");

            migrationBuilder.CreateIndex(
                name: "IX_SRStrain_SRSystemId",
                table: "SRStrain",
                column: "SRSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SRSegment");

            migrationBuilder.DropTable(
                name: "SRStrain");

            migrationBuilder.DropTable(
                name: "SRSystem");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Migrations
{
    public partial class CycleRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieID",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MovieID",
                table: "Actors",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_MovieID",
                table: "Actors",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_MovieID",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_MovieID",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "Actors");

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    CastID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.CastID, x.MoviesID });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_CastID",
                        column: x => x.CastID,
                        principalTable: "Actors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_MoviesID",
                        column: x => x.MoviesID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesID",
                table: "ActorMovie",
                column: "MoviesID");
        }
    }
}

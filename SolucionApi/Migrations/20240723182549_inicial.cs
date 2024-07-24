using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolucionApi.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CountryTimeZone = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    OfficialSite = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebChannels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CountryTimeZone = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    OfficialSite = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Genres = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Runtime = table.Column<int>(type: "int", nullable: true),
                    AverageRuntime = table.Column<int>(type: "int", nullable: true),
                    Premiered = table.Column<DateTime>(type: "date", nullable: true),
                    Ended = table.Column<DateTime>(type: "date", nullable: true),
                    OfficialSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Days = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Average = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    DvdCountryName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DvdCountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DvdCountryTimeZone = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Tvrage = table.Column<int>(type: "int", maxLength: 64, nullable: true),
                    Thetvdb = table.Column<int>(type: "int", maxLength: 64, nullable: true),
                    Imdb = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Original = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    Updated = table.Column<long>(type: "bigint", nullable: true),
                    SelfHref = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousEpisodeHref = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousEpisodeName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NextEpisodeHref = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextEpisodeName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NetworkId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    WebChannelId = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shows_Networks_NetworkId",
                        column: x => x.NetworkId,
                        principalTable: "Networks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shows_WebChannels_WebChannelId",
                        column: x => x.WebChannelId,
                        principalTable: "WebChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shows_NetworkId",
                table: "Shows",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_WebChannelId",
                table: "Shows",
                column: "WebChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "Networks");

            migrationBuilder.DropTable(
                name: "WebChannels");
        }
    }
}

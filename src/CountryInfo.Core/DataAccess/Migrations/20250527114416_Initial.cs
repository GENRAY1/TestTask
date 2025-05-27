using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountryInfo.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, collation: "NOCASE"),
                    Region = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Subregion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Capital = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Population = table.Column<long>(type: "INTEGER", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Statistic_TotalRequests = table.Column<int>(type: "INTEGER", nullable: false),
                    Statistic_LastRequestedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Statistic_AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                table: "Country",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}

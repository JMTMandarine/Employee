using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Sample.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblEmployee",
                columns: table => new
                {
                    _email = table.Column<string>(type: "VARCHAR(320)", maxLength: 320, nullable: false),
                    _name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    _tell = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    _joined = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployee", x => x._email);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblEmployee__name",
                table: "TblEmployee",
                column: "_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblEmployee");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuNhua.Migrations
{
    /// <inheritdoc />
    public partial class HinhAnh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "HangHoas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "HangHoas");
        }
    }
}

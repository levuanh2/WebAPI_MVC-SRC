using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuNhua.Migrations
{
    /// <inheritdoc />
    public partial class Giohang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GioHangDBs",
                columns: table => new
                {
                    MaGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangDBs", x => x.MaGioHang);
                    table.ForeignKey(
                        name: "FK_GioHangDBs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTietDBs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHangHoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTietDBs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangChiTietDBs_GioHangDBs_MaGioHang",
                        column: x => x.MaGioHang,
                        principalTable: "GioHangDBs",
                        principalColumn: "MaGioHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHangChiTietDBs_HangHoas_MaHangHoa",
                        column: x => x.MaHangHoa,
                        principalTable: "HangHoas",
                        principalColumn: "MaHangHoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTietDBs_MaGioHang",
                table: "GioHangChiTietDBs",
                column: "MaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTietDBs_MaHangHoa",
                table: "GioHangChiTietDBs",
                column: "MaHangHoa");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangDBs_UserId",
                table: "GioHangDBs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GioHangChiTietDBs");

            migrationBuilder.DropTable(
                name: "GioHangDBs");
        }
    }
}

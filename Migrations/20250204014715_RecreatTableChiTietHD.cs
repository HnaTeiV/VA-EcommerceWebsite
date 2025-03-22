using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VA_EcommerceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class RecreatTableChiTietHD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders",
                table: "ChiTietHD");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products",
                table: "ChiTietHD");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "ChiTietHD");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHD_MaHD",
                table: "ChiTietHD");

            migrationBuilder.DropColumn(
                name: "MaCT",
                table: "ChiTietHD");

            migrationBuilder.DropColumn(
                name: "DonGia",
                table: "ChiTietHD");

            migrationBuilder.DropColumn(
                name: "GiamGia",
                table: "ChiTietHD");

            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "ChiTietHD");

            migrationBuilder.AddColumn<int>(
                name: "TongTien",
                table: "ChiTietHD",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__ChiTietH__7557FC8E701B7A0D",
                table: "ChiTietHD",
                columns: new[] { "MaHD", "MaHH" });

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietHD__MaHD__1B9317B3",
                table: "ChiTietHD",
                column: "MaHD",
                principalTable: "HoaDon",
                principalColumn: "MaHD");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietHD__MaHH__1C873BEC",
                table: "ChiTietHD",
                column: "MaHH",
                principalTable: "HangHoa",
                principalColumn: "MaHH");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietHD__MaHD__1B9317B3",
                table: "ChiTietHD");

            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietHD__MaHH__1C873BEC",
                table: "ChiTietHD");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ChiTietH__7557FC8E701B7A0D",
                table: "ChiTietHD");

            migrationBuilder.DropColumn(
                name: "TongTien",
                table: "ChiTietHD");

            migrationBuilder.AddColumn<int>(
                name: "MaCT",
                table: "ChiTietHD",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "DonGia",
                table: "ChiTietHD",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GiamGia",
                table: "ChiTietHD",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "ChiTietHD",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "ChiTietHD",
                column: "MaCT");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHD_MaHD",
                table: "ChiTietHD",
                column: "MaHD");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders",
                table: "ChiTietHD",
                column: "MaHD",
                principalTable: "HoaDon",
                principalColumn: "MaHD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products",
                table: "ChiTietHD",
                column: "MaHH",
                principalTable: "HangHoa",
                principalColumn: "MaHH");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VA_EcommerceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class remakeEmployeeAndDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__PhongBan__2725E7E48F473923",
                table: "PhongBan");

            migrationBuilder.DropPrimaryKey(
                name: "PK__NhanVien__2725D70AC5F44475",
                table: "NhanVien");

            migrationBuilder.RenameIndex(
                name: "UQ__NhanVien__A9D105340B61E937",
                table: "NhanVien",
                newName: "UQ__NhanVien__A9D10534C841FFF4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "NhanVien",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "GioiTinh",
                table: "NhanVien",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__PhongBan__2725E7E4CCF35283",
                table: "PhongBan",
                column: "MaPB");

            migrationBuilder.AddPrimaryKey(
                name: "PK__NhanVien__2725D70A7C859924",
                table: "NhanVien",
                column: "MaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__PhongBan__2725E7E4CCF35283",
                table: "PhongBan");

            migrationBuilder.DropPrimaryKey(
                name: "PK__NhanVien__2725D70A7C859924",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "NhanVien");

            migrationBuilder.RenameIndex(
                name: "UQ__NhanVien__A9D10534C841FFF4",
                table: "NhanVien",
                newName: "UQ__NhanVien__A9D105340B61E937");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "NhanVien",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK__PhongBan__2725E7E48F473923",
                table: "PhongBan",
                column: "MaPB");

            migrationBuilder.AddPrimaryKey(
                name: "PK__NhanVien__2725D70AC5F44475",
                table: "NhanVien",
                column: "MaNV");
        }
    }
}

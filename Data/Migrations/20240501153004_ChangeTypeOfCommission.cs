using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class ChangeTypeOfCommission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerBrokerage",
                table: "Tbl_PropertyDeal",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientBrokerage",
                table: "Tbl_PropertyDeal",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Comission",
                table: "Tbl_Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Tbl_Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6460));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6469));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6470));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6472));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6478));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6480));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6483));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6486));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6488));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6489));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6491));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6492));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6494));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6495));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6499));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6500));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 1, 21, 0, 2, 665, DateTimeKind.Local).AddTicks(6501));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OwnerBrokerage",
                table: "Tbl_PropertyDeal",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ClientBrokerage",
                table: "Tbl_PropertyDeal",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Comission",
                table: "Tbl_Property",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Tbl_Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4227));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4358));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4359));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4361));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4364));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4364));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4366));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4367));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4368));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4368));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4369));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4369));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4370));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4370));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4372));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4372));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4373));

            migrationBuilder.UpdateData(
                table: "Tbl_Role_Permission",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4373));
        }
    }
}

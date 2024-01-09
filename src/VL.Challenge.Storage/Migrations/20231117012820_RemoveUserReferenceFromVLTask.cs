using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VL.Challenge.Storage.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserReferenceFromVLTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 17, 3, 58, 20, 734, DateTimeKind.Unspecified).AddTicks(3971), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 17, 3, 38, 20, 734, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 18, 3, 58, 20, 734, DateTimeKind.Unspecified).AddTicks(3978), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 18, 3, 38, 20, 734, DateTimeKind.Unspecified).AddTicks(3975), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 17, 3, 8, 20, 734, DateTimeKind.Unspecified).AddTicks(3982), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 17, 2, 58, 20, 734, DateTimeKind.Unspecified).AddTicks(3981), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 17, 0, 20, 5, 758, DateTimeKind.Unspecified).AddTicks(3713), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 17, 0, 0, 5, 758, DateTimeKind.Unspecified).AddTicks(3653), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 18, 0, 20, 5, 758, DateTimeKind.Unspecified).AddTicks(3733), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 18, 0, 0, 5, 758, DateTimeKind.Unspecified).AddTicks(3718), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 16, 23, 30, 5, 758, DateTimeKind.Unspecified).AddTicks(3737), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 16, 23, 20, 5, 758, DateTimeKind.Unspecified).AddTicks(3735), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

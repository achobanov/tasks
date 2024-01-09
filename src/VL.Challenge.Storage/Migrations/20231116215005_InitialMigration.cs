using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VL.Challenge.Storage.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "EndTime", "StartTime", "Subject", "UserId" },
                values: new object[,]
                {
                    { 1, "Task 1 Description", new DateTimeOffset(new DateTime(2023, 11, 17, 0, 20, 5, 758, DateTimeKind.Unspecified).AddTicks(3713), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 17, 0, 0, 5, 758, DateTimeKind.Unspecified).AddTicks(3653), new TimeSpan(0, 2, 0, 0, 0)), "Task 1", 1 },
                    { 2, "Task 2 Description", new DateTimeOffset(new DateTime(2023, 11, 18, 0, 20, 5, 758, DateTimeKind.Unspecified).AddTicks(3733), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 18, 0, 0, 5, 758, DateTimeKind.Unspecified).AddTicks(3718), new TimeSpan(0, 2, 0, 0, 0)), "Task 2", 1 },
                    { 3, null, new DateTimeOffset(new DateTime(2023, 11, 16, 23, 30, 5, 758, DateTimeKind.Unspecified).AddTicks(3737), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 16, 23, 20, 5, 758, DateTimeKind.Unspecified).AddTicks(3735), new TimeSpan(0, 2, 0, 0, 0)), "Task 3", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

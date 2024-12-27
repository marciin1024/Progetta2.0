using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Progetta.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6349));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6351));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6352));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6253));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6257));

            migrationBuilder.UpdateData(
                table: "TasksToDo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6275));

            migrationBuilder.UpdateData(
                table: "TasksToDo",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DueDate" },
                values: new object[] { new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6278), new DateTime(2024, 12, 27, 20, 55, 41, 606, DateTimeKind.Local).AddTicks(6282) });

            migrationBuilder.UpdateData(
                table: "TasksToDo",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DueDate" },
                values: new object[] { new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6326), new DateTime(2024, 12, 27, 20, 55, 41, 606, DateTimeKind.Local).AddTicks(6327) });

            migrationBuilder.UpdateData(
                table: "TasksToDo",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6329));

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumns: new[] { "ProjectId", "UsernameId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 19, 55, 41, 606, DateTimeKind.Utc).AddTicks(6401));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "LabelId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "LabelId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "LabelId",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4423));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4424));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4305));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4309));

            migrationBuilder.UpdateData(
                table: "TasksToDo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4335));

            migrationBuilder.UpdateData(
                table: "TasksToDo",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DueDate" },
                values: new object[] { new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4337), new DateTime(2024, 12, 15, 12, 7, 47, 513, DateTimeKind.Local).AddTicks(4343) });

            migrationBuilder.UpdateData(
                table: "TasksToDo",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DueDate" },
                values: new object[] { new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4391), new DateTime(2024, 12, 15, 12, 7, 47, 513, DateTimeKind.Local).AddTicks(4392) });

            migrationBuilder.UpdateData(
                table: "TasksToDo",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4394));

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumns: new[] { "ProjectId", "UsernameId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedAt",
                value: new DateTime(2024, 12, 15, 11, 7, 47, 513, DateTimeKind.Utc).AddTicks(4498));
        }
    }
}

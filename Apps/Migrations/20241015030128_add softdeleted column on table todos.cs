using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apps.Migrations
{
    /// <inheritdoc />
    public partial class addsoftdeletedcolumnontabletodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "todos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "todos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_todos_is_deleted",
                table: "todos",
                column: "is_deleted",
                filter: "is_deleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_todos_is_deleted",
                table: "todos");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "todos");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "todos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entrance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Interviews",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Examinations",
                newName: "RecordedById");

            migrationBuilder.AddColumn<int>(
                name: "RecordedById",
                table: "Interviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Examinations",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordedById",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Examinations");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Interviews",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "RecordedById",
                table: "Examinations",
                newName: "UserId");
        }
    }
}

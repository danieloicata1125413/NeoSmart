using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoSmart.Data.Migrations
{
    /// <inheritdoc />
    public partial class TrainingUpdateIsoftdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trainings_Cod",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_OccupationId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Occupations_Cod",
                table: "Occupations");

            migrationBuilder.DropIndex(
                name: "IX_Occupations_ProcessId",
                table: "Occupations");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Trainings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Trainings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_OccupationId_Cod",
                table: "Trainings",
                columns: new[] { "OccupationId", "Cod" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_ProcessId_Cod",
                table: "Occupations",
                columns: new[] { "ProcessId", "Cod" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trainings_OccupationId_Cod",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Occupations_ProcessId_Cod",
                table: "Occupations");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Trainings");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_Cod",
                table: "Trainings",
                column: "Cod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_OccupationId",
                table: "Trainings",
                column: "OccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_Cod",
                table: "Occupations",
                column: "Cod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_ProcessId",
                table: "Occupations",
                column: "ProcessId");
        }
    }
}

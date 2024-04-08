using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoSmart.Data.Migrations
{
    /// <inheritdoc />
    public partial class deletetrainingcalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_TrainingCalendar_TrainingCalendarId",
                table: "Trainings");

            migrationBuilder.DropTable(
                name: "TrainingCalendar");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_TrainingCalendarId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingCalendarId",
                table: "Trainings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingCalendarId",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrainingCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCalendar", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainingCalendarId",
                table: "Trainings",
                column: "TrainingCalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_TrainingCalendar_TrainingCalendarId",
                table: "Trainings",
                column: "TrainingCalendarId",
                principalTable: "TrainingCalendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

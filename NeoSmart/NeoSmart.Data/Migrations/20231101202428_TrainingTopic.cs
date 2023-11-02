using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoSmart.Data.Migrations
{
    /// <inheritdoc />
    public partial class TrainingTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Topics",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_Name",
                table: "Topics",
                newName: "IX_Topics_Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Topics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Topics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Topics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Topics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TrainingTopic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTopic_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingTopic_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTopic_TopicId",
                table: "TrainingTopic",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTopic_TrainingId",
                table: "TrainingTopic",
                column: "TrainingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingTopic");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Topics");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Topics",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_Description",
                table: "Topics",
                newName: "IX_Topics_Name");
        }
    }
}

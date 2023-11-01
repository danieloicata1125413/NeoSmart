using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeoSmart.Data.Migrations
{
    /// <inheritdoc />
    public partial class Trainingupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trainings_Name",
                table: "Trainings");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Trainings",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Cod",
                table: "Trainings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OccupationId",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_Cod",
                table: "Trainings",
                column: "Cod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_OccupationId",
                table: "Trainings",
                column: "OccupationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Occupations_OccupationId",
                table: "Trainings",
                column: "OccupationId",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Occupations_OccupationId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_Cod",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_OccupationId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Cod",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "OccupationId",
                table: "Trainings");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Trainings",
                newName: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_Name",
                table: "Trainings",
                column: "Name",
                unique: true);
        }
    }
}

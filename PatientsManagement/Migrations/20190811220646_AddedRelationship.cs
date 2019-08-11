using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientsManagement.Migrations
{
    public partial class AddedRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalContact_Patients_PatientId",
                table: "AdditionalContact");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "AdditionalContact",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalContact_Patients_PatientId",
                table: "AdditionalContact",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalContact_Patients_PatientId",
                table: "AdditionalContact");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "AdditionalContact",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalContact_Patients_PatientId",
                table: "AdditionalContact",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

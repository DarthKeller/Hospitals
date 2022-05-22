using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApplication.Migrations
{
    public partial class InsuranceAddressSpecialtyv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "InsuranceProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalInsuranceProvider",
                columns: table => new
                {
                    InNetworkHospitalsId = table.Column<int>(type: "int", nullable: false),
                    PreferredProvidersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalInsuranceProvider", x => new { x.InNetworkHospitalsId, x.PreferredProvidersId });
                    table.ForeignKey(
                        name: "FK_HospitalInsuranceProvider_Hospitals_InNetworkHospitalsId",
                        column: x => x.InNetworkHospitalsId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalInsuranceProvider_InsuranceProviders_PreferredProvidersId",
                        column: x => x.PreferredProvidersId,
                        principalTable: "InsuranceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HospitalSpecialty",
                columns: table => new
                {
                    HospitalsId = table.Column<int>(type: "int", nullable: false),
                    SpecialtiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalSpecialty", x => new { x.HospitalsId, x.SpecialtiesId });
                    table.ForeignKey(
                        name: "FK_HospitalSpecialty_Hospitals_HospitalsId",
                        column: x => x.HospitalsId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalSpecialty_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HospitalInsuranceProvider_PreferredProvidersId",
                table: "HospitalInsuranceProvider",
                column: "PreferredProvidersId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalSpecialty_SpecialtiesId",
                table: "HospitalSpecialty",
                column: "SpecialtiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalInsuranceProvider");

            migrationBuilder.DropTable(
                name: "HospitalSpecialty");

            migrationBuilder.DropTable(
                name: "InsuranceProviders");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Hospitals");
        }
    }
}

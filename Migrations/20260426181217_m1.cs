using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Seguridad.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipamiento",
                columns: table => new
                {
                    Id_Equipamiento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre_Equipo = table.Column<string>(type: "text", nullable: false),
                    CodigoEquipamiento = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamiento", x => x.Id_Equipamiento);
                });

            migrationBuilder.CreateTable(
                name: "IdentidadAcceso",
                columns: table => new
                {
                    Id_Identidad = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoPersona = table.Column<string>(type: "text", nullable: false),
                    TipoPersona = table.Column<string>(type: "text", nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentidadAcceso", x => x.Id_Identidad);
                });

            migrationBuilder.CreateTable(
                name: "AsignacionEquipamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    identidadId = table.Column<int>(type: "integer", nullable: false),
                    equipamientoId = table.Column<int>(type: "integer", nullable: false),
                    fechaEntrega = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Id_Identidad = table.Column<int>(type: "integer", nullable: false),
                    Id_Equipamiento = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionEquipamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionEquipamientos_Equipamiento_Id_Equipamiento",
                        column: x => x.Id_Equipamiento,
                        principalTable: "Equipamiento",
                        principalColumn: "Id_Equipamiento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionEquipamientos_IdentidadAcceso_Id_Identidad",
                        column: x => x.Id_Identidad,
                        principalTable: "IdentidadAcceso",
                        principalColumn: "Id_Identidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionEquipamientos_Id_Equipamiento",
                table: "AsignacionEquipamientos",
                column: "Id_Equipamiento");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionEquipamientos_Id_Identidad",
                table: "AsignacionEquipamientos",
                column: "Id_Identidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionEquipamientos");

            migrationBuilder.DropTable(
                name: "Equipamiento");

            migrationBuilder.DropTable(
                name: "IdentidadAcceso");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace workforcemanagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    ComputerId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d %H:%M:%S')"),
                    EmployeeId = table.Column<int>(nullable: true),
                    Make = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.ComputerId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d %H:%M:%S')"),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgram",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d %H:%M:%S')"),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgram", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ComputerId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d %H:%M:%S')"),
                    DepartmentId = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Computer_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computer",
                        principalColumn: "ComputerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendee",
                columns: table => new
                {
                    AttendeeId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d %H:%M:%S')"),
                    EmployeeId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendee", x => x.AttendeeId);
                    table.ForeignKey(
                        name: "FK_Attendee_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendee_TrainingProgram_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "TrainingProgram",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendee_EmployeeId",
                table: "Attendee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendee_ProgramId",
                table: "Attendee",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ComputerId",
                table: "Employee",
                column: "ComputerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendee");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "TrainingProgram");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}

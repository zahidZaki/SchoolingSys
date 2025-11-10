using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teacherinfo.DLL.Migrations
{
    /// <inheritdoc />
    public partial class testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherTable",
                columns: table => new
                {
                    Teacher_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Teacher_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Teacher_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Teacher_Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Teacher_Hire_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Teacher_Qualification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Teacher_Is_Active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Teacher_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherTable");
        }
    }
}

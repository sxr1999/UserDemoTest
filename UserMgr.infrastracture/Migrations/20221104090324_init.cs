using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserMgr.infrastracture.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_UserAccessFailConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PhoneNumber_RegionNumber = table.Column<int>(type: "int", unicode: false, maxLength: 5, nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UserAccessFailConfigs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    phoneNumber_RegionNumber = table.Column<int>(type: "int", unicode: false, maxLength: 5, nullable: false),
                    phoneNumber_Number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    passwordHash = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_UserAccessFails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LockoutEnd = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    lockOut = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UserAccessFails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_UserAccessFails_T_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "T_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_T_UserAccessFails_UserId",
                table: "T_UserAccessFails",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_UserAccessFailConfigs");

            migrationBuilder.DropTable(
                name: "T_UserAccessFails");

            migrationBuilder.DropTable(
                name: "T_Users");
        }
    }
}

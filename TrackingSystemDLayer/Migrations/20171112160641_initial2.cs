using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TrackingSystemDLayer.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_User<int>Id",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Role<int>Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Role<int>Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_User<int>Id",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Role<int>Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "User<int>Id",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.AddColumn<int>(
                name: "Role<int>Id",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "User<int>Id",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role<int>Id",
                table: "Users",
                column: "Role<int>Id");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_User<int>Id",
                table: "Roles",
                column: "User<int>Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_User<int>Id",
                table: "Roles",
                column: "User<int>Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Role<int>Id",
                table: "Users",
                column: "Role<int>Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Catalog.Api.Migrations
{
    public partial class removeunusedtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUserClaimModel_CompanyModel_CompanyId",
                table: "CompanyUserClaimModel");

            migrationBuilder.DropTable(
                name: "CompanyModel");

            migrationBuilder.DropTable(
                name: "CompanyUsersClaims");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "RoleDbModel");

            migrationBuilder.DropTable(
                name: "RoleUsersClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "Users",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimValue",
                table: "CompanyUserClaimModel",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "CompanyUserClaimModel",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUserClaimModel_Companies_CompanyId",
                table: "CompanyUserClaimModel",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUserClaimModel_Companies_CompanyId",
                table: "CompanyUserClaimModel");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimValue",
                table: "CompanyUserClaimModel",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "CompanyUserClaimModel",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.CreateTable(
                name: "CompanyModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUsersClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: false),
                    ClaimValue = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUsersClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleDbModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDbModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleUsersClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsersClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUsersClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    IdClaim = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: false),
                    ClaimValue = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.IdClaim);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    IdClaim = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: false),
                    ClaimValue = table.Column<string>(type: "text", nullable: false),
                    IdRole = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.IdClaim);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_IdRole",
                table: "RoleClaims",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDbModel_NormalizedName",
                table: "RoleDbModel",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsersClaims_UserId",
                table: "RoleUsersClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUserClaimModel_CompanyModel_CompanyId",
                table: "CompanyUserClaimModel",
                column: "CompanyId",
                principalTable: "CompanyModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

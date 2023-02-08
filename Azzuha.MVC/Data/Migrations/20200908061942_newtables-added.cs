using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Azzuha.AdminPanel.Data.Migrations
{
    public partial class newtablesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationBy",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableFlag",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnalbleFalg",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlock",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UnhashPassword",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdationBy",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdationDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EnableFlag",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EnalbleFalg",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsBlock",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UnhashPassword",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdationBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdationDate",
                table: "AspNetUsers");
        }
    }
}

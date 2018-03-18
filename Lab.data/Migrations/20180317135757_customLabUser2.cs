using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lab.Data.Migrations
{
    public partial class customLabUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameForLabApp",
                table: "AspNetUsers",
                newName: "NameLab");

            migrationBuilder.AlterColumn<string>(
                name: "NameLab",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameLab",
                table: "AspNetUsers",
                newName: "NameForLabApp");

            migrationBuilder.AlterColumn<string>(
                name: "NameForLabApp",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}

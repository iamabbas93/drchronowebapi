using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrChronoWebAPI.Migrations
{
    public partial class updateTableClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "drcClientsTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                table: "drcClientsTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "drcClientsTokens",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "drcClientsTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateBy",
                table: "drcClientsTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updateDate",
                table: "drcClientsTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                table: "drcClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdDate",
                table: "drcClients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "drcClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateBy",
                table: "drcClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updateDate",
                table: "drcClients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "drcClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "drcClientsTokens");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "drcClientsTokens");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "drcClientsTokens");

            migrationBuilder.DropColumn(
                name: "state",
                table: "drcClientsTokens");

            migrationBuilder.DropColumn(
                name: "updateBy",
                table: "drcClientsTokens");

            migrationBuilder.DropColumn(
                name: "updateDate",
                table: "drcClientsTokens");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "drcClients");

            migrationBuilder.DropColumn(
                name: "createdDate",
                table: "drcClients");

            migrationBuilder.DropColumn(
                name: "password",
                table: "drcClients");

            migrationBuilder.DropColumn(
                name: "updateBy",
                table: "drcClients");

            migrationBuilder.DropColumn(
                name: "updateDate",
                table: "drcClients");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "drcClients");
        }
    }
}

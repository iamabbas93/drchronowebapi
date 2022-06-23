using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrChronoWebAPI.Migrations
{
    public partial class tblClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "drcClients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientSecret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authorizationEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tokenEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    redirectUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drcClients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "drcClientsTokens",
                columns: table => new
                {
                    tokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorId = table.Column<int>(type: "int", nullable: true),
                    access_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expires_in = table.Column<long>(type: "bigint", nullable: true),
                    token_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    scope = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    expiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drcClientsTokens", x => x.tokenId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "drcClients");

            migrationBuilder.DropTable(
                name: "drcClientsTokens");
        }
    }
}

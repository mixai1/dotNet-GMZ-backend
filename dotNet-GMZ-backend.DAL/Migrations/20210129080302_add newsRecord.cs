using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotNet_GMZ_backend.DAL.Migrations
{
    public partial class addnewsRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserAppId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsRecords_AspNetUsers_UserAppId",
                        column: x => x.UserAppId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsRecords_UserAppId",
                table: "NewsRecords",
                column: "UserAppId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsRecords");
        }
    }
}

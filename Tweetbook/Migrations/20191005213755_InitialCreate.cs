using System;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweetbook.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                },
                constraints: table => { table.PrimaryKey("PK_Posts", x => x.id); });
                
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}

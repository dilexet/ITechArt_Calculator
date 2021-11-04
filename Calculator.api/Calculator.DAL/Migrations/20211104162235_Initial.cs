using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expressions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatorA = table.Column<double>(type: "float", nullable: false),
                    OperatorB = table.Column<double>(type: "float", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expressions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expressions");
        }
    }
}

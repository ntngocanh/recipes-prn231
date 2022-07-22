using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessObjects.Migrations
{
    public partial class ReactionTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReactionType",
                table: "Reactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReactionType",
                table: "Reactions");
        }
    }
}

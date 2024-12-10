using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAndProjet.API.Migrations
{
    public partial class AddUserTasksRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FullUserId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FullUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectsModelsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Projects_ProjectsModelsId",
                        column: x => x.ProjectsModelsId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FullUserId",
                table: "Tasks",
                column: "FullUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectsModelsId",
                table: "Users",
                column: "ProjectsModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_FullUsers_FullUserId",
                table: "Tasks",
                column: "FullUserId",
                principalTable: "FullUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_FullUsers_FullUserId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "FullUsers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_FullUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "FullUserId",
                table: "Tasks");
        }
    }
}

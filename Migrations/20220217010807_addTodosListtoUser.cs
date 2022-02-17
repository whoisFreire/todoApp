using Microsoft.EntityFrameworkCore.Migrations;

namespace todoApp.Migrations
{
    public partial class addTodosListtoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todos");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UserForeignKey",
                table: "Todos",
                column: "UserForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserForeignKey",
                table: "Todos",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserForeignKey",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserForeignKey",
                table: "Todos");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Todos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UserId",
                table: "Todos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace todoApp.Migrations
{
    public partial class CreatingRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Todos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserForeignKey",
                table: "Todos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserForeignKey",
                table: "Todos");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Todos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}

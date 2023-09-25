using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Migrations
{
    public partial class movieIdBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "ShoppingCartItems",
                newName: "Id");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "ShoppingCartItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_MovieId",
                table: "ShoppingCartItems",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Movies_MovieId",
                table: "ShoppingCartItems",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Movies_MovieId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_MovieId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShoppingCartItems",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}

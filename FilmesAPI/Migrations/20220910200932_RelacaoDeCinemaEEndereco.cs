using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    public partial class RelacaoDeCinemaEEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnderecoFK",
                table: "Cinemas");

            migrationBuilder.RenameColumn(
                name: "GerenteFK",
                table: "Cinemas",
                newName: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_EnderecoId",
                table: "Cinemas",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Enderecos_EnderecoId",
                table: "Cinemas",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Enderecos_EnderecoId",
                table: "Cinemas");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_EnderecoId",
                table: "Cinemas");

            migrationBuilder.RenameColumn(
                name: "EnderecoId",
                table: "Cinemas",
                newName: "GerenteFK");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoFK",
                table: "Cinemas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

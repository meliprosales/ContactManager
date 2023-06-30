using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AddContactManagerCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactManagerId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContactManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactManagers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactManagerId",
                table: "Contacts",
                column: "ContactManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactManagers_ContactManagerId",
                table: "Contacts",
                column: "ContactManagerId",
                principalTable: "ContactManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactManagers_ContactManagerId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactManagers");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ContactManagerId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactManagerId",
                table: "Contacts");
        }
    }
}

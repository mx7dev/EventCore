using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsRegistration.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEventRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Events",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Events");
        }
    }
}

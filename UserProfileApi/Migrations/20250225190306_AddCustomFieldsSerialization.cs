using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProfileApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomFieldsSerialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomFields",
                table: "UserProfiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomFields",
                table: "UserProfiles");
        }
    }
}

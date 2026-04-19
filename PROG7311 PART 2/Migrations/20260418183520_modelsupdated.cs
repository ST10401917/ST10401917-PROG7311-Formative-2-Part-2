using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG7311_PART_2.Migrations
{
    /// <inheritdoc />
    public partial class modelsupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractName",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractName",
                table: "ServiceRequests");
        }
    }
}

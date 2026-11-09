using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbpApiBook.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBookAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "BookStoreApi");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "BookStoreApi");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "BookStoreApi");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "BookStoreApi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "BookStoreApi",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "BookStoreApi",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "BookStoreApi",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "BookStoreApi",
                type: "uuid",
                nullable: true);
        }
    }
}

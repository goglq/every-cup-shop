using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveryCupShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCupShapeAndCupAttachmentEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cup_CupAttachments_CupAttachmentId",
                table: "Cup");

            migrationBuilder.DropForeignKey(
                name: "FK_Cup_CupShapes_CupShapeId",
                table: "Cup");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Cup_CupId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "CupAttachments");

            migrationBuilder.DropTable(
                name: "CupShapes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cup",
                table: "Cup");

            migrationBuilder.DropIndex(
                name: "IX_Cup_CupAttachmentId",
                table: "Cup");

            migrationBuilder.DropIndex(
                name: "IX_Cup_CupShapeId",
                table: "Cup");

            migrationBuilder.DropColumn(
                name: "CupAttachmentId",
                table: "Cup");

            migrationBuilder.DropColumn(
                name: "CupShapeId",
                table: "Cup");

            migrationBuilder.RenameTable(
                name: "Cup",
                newName: "Cups");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Cups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Cups",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cups",
                table: "Cups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Cups_CupId",
                table: "OrderItems",
                column: "CupId",
                principalTable: "Cups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Cups_CupId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cups",
                table: "Cups");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Cups");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cups");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cups");

            migrationBuilder.RenameTable(
                name: "Cups",
                newName: "Cup");

            migrationBuilder.AddColumn<Guid>(
                name: "CupAttachmentId",
                table: "Cup",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CupShapeId",
                table: "Cup",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cup",
                table: "Cup",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CupAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupAttachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CupShapes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupShapes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cup_CupAttachmentId",
                table: "Cup",
                column: "CupAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cup_CupShapeId",
                table: "Cup",
                column: "CupShapeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cup_CupAttachments_CupAttachmentId",
                table: "Cup",
                column: "CupAttachmentId",
                principalTable: "CupAttachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cup_CupShapes_CupShapeId",
                table: "Cup",
                column: "CupShapeId",
                principalTable: "CupShapes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Cup_CupId",
                table: "OrderItems",
                column: "CupId",
                principalTable: "Cup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

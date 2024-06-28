using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EveryCupShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCupsToDbContext : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cup",
                table: "Cup");

            migrationBuilder.RenameTable(
                name: "Cup",
                newName: "Cups");

            migrationBuilder.RenameIndex(
                name: "IX_Cup_CupShapeId",
                table: "Cups",
                newName: "IX_Cups_CupShapeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cup_CupAttachmentId",
                table: "Cups",
                newName: "IX_Cups_CupAttachmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cups",
                table: "Cups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cups_CupAttachments_CupAttachmentId",
                table: "Cups",
                column: "CupAttachmentId",
                principalTable: "CupAttachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cups_CupShapes_CupShapeId",
                table: "Cups",
                column: "CupShapeId",
                principalTable: "CupShapes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Cups_CupAttachments_CupAttachmentId",
                table: "Cups");

            migrationBuilder.DropForeignKey(
                name: "FK_Cups_CupShapes_CupShapeId",
                table: "Cups");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Cups_CupId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cups",
                table: "Cups");

            migrationBuilder.RenameTable(
                name: "Cups",
                newName: "Cup");

            migrationBuilder.RenameIndex(
                name: "IX_Cups_CupShapeId",
                table: "Cup",
                newName: "IX_Cup_CupShapeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cups_CupAttachmentId",
                table: "Cup",
                newName: "IX_Cup_CupAttachmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cup",
                table: "Cup",
                column: "Id");

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

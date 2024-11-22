using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tech_Manage_Server.Migrations
{
    /// <inheritdoc />
    public partial class editInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairItems_Inventorys_ItemId",
                table: "RepairItems");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "RepairItems",
                newName: "InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairItems_ItemId",
                table: "RepairItems",
                newName: "IX_RepairItems_InventoryId");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "Inventorys",
                newName: "InventoryName");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Inventorys",
                newName: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairItems_Inventorys_InventoryId",
                table: "RepairItems",
                column: "InventoryId",
                principalTable: "Inventorys",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairItems_Inventorys_InventoryId",
                table: "RepairItems");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "RepairItems",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairItems_InventoryId",
                table: "RepairItems",
                newName: "IX_RepairItems_ItemId");

            migrationBuilder.RenameColumn(
                name: "InventoryName",
                table: "Inventorys",
                newName: "ItemName");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "Inventorys",
                newName: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairItems_Inventorys_ItemId",
                table: "RepairItems",
                column: "ItemId",
                principalTable: "Inventorys",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

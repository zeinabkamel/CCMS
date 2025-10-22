using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCMS.Migrations
{
    /// <inheritdoc />
    public partial class Added_Stores_RawMaterials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterials_Stores_StoreId1",
                table: "RawMaterials");

            migrationBuilder.DropIndex(
                name: "IX_RawMaterials_StoreId1",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CurrentQty",
                table: "RawMaterials");

            migrationBuilder.RenameColumn(
                name: "StoreId1",
                table: "RawMaterials",
                newName: "LastModifierId");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Stores",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Stores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Stores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Stores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Stores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Stores",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Stores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Stores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "RawMaterials",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReorderLevel",
                table: "RawMaterials",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "RawMaterials",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RawMaterials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "RawMaterials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "RawMaterials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RawMaterials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "RawMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RawMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "RawMaterials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "RawMaterials",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "RawMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                table: "RawMaterials",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "SupplierName",
                table: "RawMaterials");

            migrationBuilder.RenameColumn(
                name: "LastModifierId",
                table: "RawMaterials",
                newName: "StoreId1");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Unit",
                table: "RawMaterials",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReorderLevel",
                table: "RawMaterials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentQty",
                table: "RawMaterials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterials_StoreId1",
                table: "RawMaterials",
                column: "StoreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterials_Stores_StoreId1",
                table: "RawMaterials",
                column: "StoreId1",
                principalTable: "Stores",
                principalColumn: "Id");
        }
    }
}

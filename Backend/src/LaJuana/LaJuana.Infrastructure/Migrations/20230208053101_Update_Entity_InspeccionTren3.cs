using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaJuana.Infrastructure.Migrations
{
    public partial class Update_Entity_InspeccionTren3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "InspectionTrains",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "InspectionTrains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryId",
                table: "InspectionTrains",
                type: "uniqueidentifier",
                nullable: false);

            //migrationBuilder.CreateIndex(
            //    name: "IX_InspectionTrains_CategoryId",
            //    table: "InspectionTrains",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_InspectionTrains_SubCategoryId",
            //    table: "InspectionTrains",
            //    column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionTrains_Categories_CategoryId",
                table: "InspectionTrains",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionTrains_Categories_SubCategoryId",
                table: "InspectionTrains",
                column: "SubCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionTrains_Categories_CategoryId",
                table: "InspectionTrains");

            migrationBuilder.DropForeignKey(
                name: "FK_InspectionTrains_Categories_SubCategoryId",
                table: "InspectionTrains");

            //migrationBuilder.DropIndex(
            //    name: "IX_InspectionTrains_CategoryId",
            //    table: "InspectionTrains");

            //migrationBuilder.DropIndex(
            //    name: "IX_InspectionTrains_SubCategoryId",
            //    table: "InspectionTrains");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "InspectionTrains");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "InspectionTrains");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "InspectionTrains");
        }
    }
}

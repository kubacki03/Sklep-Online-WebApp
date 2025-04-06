using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class zmiany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adres",
                table: "Zamowienia");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Zamowienia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Kategoria",
                table: "Produkty",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Produkty",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Klienci",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Zamowienia");

            migrationBuilder.DropColumn(
                name: "Kategoria",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "Klienci");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Zamowienia",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

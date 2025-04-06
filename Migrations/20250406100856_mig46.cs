using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class mig46 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produkty",
                columns: new[] { "IdProduktu", "Cena", "Ilosc", "Kategoria", "Nazwa", "Opis", "Producent" },
                values: new object[,]
                {
                    { 1, 3500.0, 23, "Telefony", "Iphone 15 Pro", "Najnowszy model od Apple", "Apple" },
                    { 2, 5200.0, 12, "Laptopy", "ThinkPad X1 Carbon", "Lekki i wytrzymały laptop biznesowy", "Lenovo" },
                    { 3, 1500.0, 5, "Muzyka", "Keyboard Yamaha PSR-E373", "61-klawiszowy keyboard dla początkujących i zaawansowanych", "Yamaha" },
                    { 4, 2999.0, 18, "Telefony", "Google Pixel 8", "Telefon z czystym Androidem i świetnym aparatem", "Google" },
                    { 5, 2899.0, 10, "Tablety", "iPad 10 gen", "Tablet Apple z ekranem Retina", "Apple" },
                    { 6, 129.0, 7, "Muzyka", "Płyta winylowa Queen - Greatest Hits", "Kolekcja największych hitów zespołu Queen", "EMI" },
                    { 7, 199.0, 50, "Programowanie", "Kurs Java", "Kompletny kurs programowania w języku Java", "Udemy" },
                    { 8, 899.0, 14, "Podzespoły", "AMD Ryzen 5 5600X", "6-rdzeniowy procesor do komputerów stacjonarnych", "AMD" },
                    { 9, 449.0, 25, "Podzespoły", "Słuchawki Razer Kraken", "Gamingowe słuchawki z dźwiękiem przestrzennym", "Razer" },
                    { 10, 2899.0, 8, "Podzespoły", "Intel Core i9-14900K", "Wydajny procesor 14. generacji", "Intel" },
                    { 11, 7999.0, 6, "Laptopy", "MacBook Pro 14\"", "Wydajny laptop Apple z czipem M3", "Apple" },
                    { 12, 499.0, 20, "Podzespoły", "Goodram 32GB DDR4", "Pamięć RAM do komputerów PC", "Goodram" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Produkty",
                keyColumn: "IdProduktu",
                keyValue: 12);
        }
    }
}

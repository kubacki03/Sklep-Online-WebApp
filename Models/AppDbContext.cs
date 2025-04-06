using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<ProduktZamowienie> ProduktyZamowienia { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }

        public DbSet<DailyZnizka> DailyZnizki { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ProduktZamowienie>()
                .HasKey(pz => pz.Id);

            modelBuilder.Entity<ProduktZamowienie>()
                .HasOne(pz => pz.Produkt)
                .WithMany(p => p.ProduktZamowienia)
                .HasForeignKey(pz => pz.IdProduktu);

            modelBuilder.Entity<ProduktZamowienie>()
                .HasOne(pz => pz.Zamowienie)
                .WithMany(z => z.ProduktZamowienia)
                .HasForeignKey(pz => pz.IdZamowienia);

            modelBuilder.Entity<Produkt>().HasData(
    new Produkt { IdProduktu = 1, Cena = 3500, Ilosc = 23, Kategoria = "Telefony", Nazwa = "Iphone 15 Pro", Opis = "Najnowszy model od Apple", Producent = "Apple" },
    new Produkt { IdProduktu = 2, Cena = 5200, Ilosc = 12, Kategoria = "Laptopy", Nazwa = "ThinkPad X1 Carbon", Opis = "Lekki i wytrzymały laptop biznesowy", Producent = "Lenovo" },
    new Produkt { IdProduktu = 3, Cena = 1500, Ilosc = 5, Kategoria = "Muzyka", Nazwa = "Keyboard Yamaha PSR-E373", Opis = "61-klawiszowy keyboard dla początkujących i zaawansowanych", Producent = "Yamaha" },
    new Produkt { IdProduktu = 4, Cena = 2999, Ilosc = 18, Kategoria = "Telefony", Nazwa = "Google Pixel 8", Opis = "Telefon z czystym Androidem i świetnym aparatem", Producent = "Google" },
    new Produkt { IdProduktu = 5, Cena = 2899, Ilosc = 10, Kategoria = "Tablety", Nazwa = "iPad 10 gen", Opis = "Tablet Apple z ekranem Retina", Producent = "Apple" },
    new Produkt { IdProduktu = 6, Cena = 129, Ilosc = 7, Kategoria = "Muzyka", Nazwa = "Płyta winylowa Queen - Greatest Hits", Opis = "Kolekcja największych hitów zespołu Queen", Producent = "EMI" },
    new Produkt { IdProduktu = 7, Cena = 199, Ilosc = 50, Kategoria = "Programowanie", Nazwa = "Kurs Java", Opis = "Kompletny kurs programowania w języku Java", Producent = "Udemy" },
    new Produkt { IdProduktu = 8, Cena = 899, Ilosc = 14, Kategoria = "Podzespoły", Nazwa = "AMD Ryzen 5 5600X", Opis = "6-rdzeniowy procesor do komputerów stacjonarnych", Producent = "AMD" },
    new Produkt { IdProduktu = 9, Cena = 449, Ilosc = 25, Kategoria = "Podzespoły", Nazwa = "Słuchawki Razer Kraken", Opis = "Gamingowe słuchawki z dźwiękiem przestrzennym", Producent = "Razer" },
    new Produkt { IdProduktu = 10, Cena = 2899, Ilosc = 8, Kategoria = "Podzespoły", Nazwa = "Intel Core i9-14900K", Opis = "Wydajny procesor 14. generacji", Producent = "Intel" },
    new Produkt { IdProduktu = 11, Cena = 7999, Ilosc = 6, Kategoria = "Laptopy", Nazwa = "MacBook Pro 14\"", Opis = "Wydajny laptop Apple z czipem M3", Producent = "Apple" },
    new Produkt { IdProduktu = 12, Cena = 499, Ilosc = 20, Kategoria = "Podzespoły", Nazwa = "Goodram 32GB DDR4", Opis = "Pamięć RAM do komputerów PC", Producent = "Goodram" }
);

        }
    }
}

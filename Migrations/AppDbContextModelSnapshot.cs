﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.Models;

#nullable disable

namespace WebApplication2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication2.Models.DailyZnizka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Data")
                        .HasColumnType("date");

                    b.Property<int>("IdKlienta")
                        .HasColumnType("int");

                    b.Property<int>("IdProduktu")
                        .HasColumnType("int");

                    b.Property<double>("Znizka")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("DailyZnizki");
                });

            modelBuilder.Entity("WebApplication2.Models.Klient", b =>
                {
                    b.Property<int>("IdKlienta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdKlienta"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdKlienta");

                    b.ToTable("Klienci");
                });

            modelBuilder.Entity("WebApplication2.Models.Produkt", b =>
                {
                    b.Property<int>("IdProduktu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProduktu"));

                    b.Property<double>("Cena")
                        .HasColumnType("float");

                    b.Property<int>("Ilosc")
                        .HasColumnType("int");

                    b.Property<string>("Kategoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProduktu");

                    b.ToTable("Produkty");

                    b.HasData(
                        new
                        {
                            IdProduktu = 1,
                            Cena = 3500.0,
                            Ilosc = 23,
                            Kategoria = "Telefony",
                            Nazwa = "Iphone 15 Pro",
                            Opis = "Najnowszy model od Apple",
                            Producent = "Apple"
                        },
                        new
                        {
                            IdProduktu = 2,
                            Cena = 5200.0,
                            Ilosc = 12,
                            Kategoria = "Laptopy",
                            Nazwa = "ThinkPad X1 Carbon",
                            Opis = "Lekki i wytrzymały laptop biznesowy",
                            Producent = "Lenovo"
                        },
                        new
                        {
                            IdProduktu = 3,
                            Cena = 1500.0,
                            Ilosc = 5,
                            Kategoria = "Muzyka",
                            Nazwa = "Keyboard Yamaha PSR-E373",
                            Opis = "61-klawiszowy keyboard dla początkujących i zaawansowanych",
                            Producent = "Yamaha"
                        },
                        new
                        {
                            IdProduktu = 4,
                            Cena = 2999.0,
                            Ilosc = 18,
                            Kategoria = "Telefony",
                            Nazwa = "Google Pixel 8",
                            Opis = "Telefon z czystym Androidem i świetnym aparatem",
                            Producent = "Google"
                        },
                        new
                        {
                            IdProduktu = 5,
                            Cena = 2899.0,
                            Ilosc = 10,
                            Kategoria = "Tablety",
                            Nazwa = "iPad 10 gen",
                            Opis = "Tablet Apple z ekranem Retina",
                            Producent = "Apple"
                        },
                        new
                        {
                            IdProduktu = 6,
                            Cena = 129.0,
                            Ilosc = 7,
                            Kategoria = "Muzyka",
                            Nazwa = "Płyta winylowa Queen - Greatest Hits",
                            Opis = "Kolekcja największych hitów zespołu Queen",
                            Producent = "EMI"
                        },
                        new
                        {
                            IdProduktu = 7,
                            Cena = 199.0,
                            Ilosc = 50,
                            Kategoria = "Programowanie",
                            Nazwa = "Kurs Java",
                            Opis = "Kompletny kurs programowania w języku Java",
                            Producent = "Udemy"
                        },
                        new
                        {
                            IdProduktu = 8,
                            Cena = 899.0,
                            Ilosc = 14,
                            Kategoria = "Podzespoły",
                            Nazwa = "AMD Ryzen 5 5600X",
                            Opis = "6-rdzeniowy procesor do komputerów stacjonarnych",
                            Producent = "AMD"
                        },
                        new
                        {
                            IdProduktu = 9,
                            Cena = 449.0,
                            Ilosc = 25,
                            Kategoria = "Podzespoły",
                            Nazwa = "Słuchawki Razer Kraken",
                            Opis = "Gamingowe słuchawki z dźwiękiem przestrzennym",
                            Producent = "Razer"
                        },
                        new
                        {
                            IdProduktu = 10,
                            Cena = 2899.0,
                            Ilosc = 8,
                            Kategoria = "Podzespoły",
                            Nazwa = "Intel Core i9-14900K",
                            Opis = "Wydajny procesor 14. generacji",
                            Producent = "Intel"
                        },
                        new
                        {
                            IdProduktu = 11,
                            Cena = 7999.0,
                            Ilosc = 6,
                            Kategoria = "Laptopy",
                            Nazwa = "MacBook Pro 14\"",
                            Opis = "Wydajny laptop Apple z czipem M3",
                            Producent = "Apple"
                        },
                        new
                        {
                            IdProduktu = 12,
                            Cena = 499.0,
                            Ilosc = 20,
                            Kategoria = "Podzespoły",
                            Nazwa = "Goodram 32GB DDR4",
                            Opis = "Pamięć RAM do komputerów PC",
                            Producent = "Goodram"
                        });
                });

            modelBuilder.Entity("WebApplication2.Models.ProduktZamowienie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdProduktu")
                        .HasColumnType("int");

                    b.Property<int>("IdZamowienia")
                        .HasColumnType("int");

                    b.Property<int>("Ilosc")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdProduktu");

                    b.HasIndex("IdZamowienia");

                    b.ToTable("ProduktyZamowienia");
                });

            modelBuilder.Entity("WebApplication2.Models.Zamowienie", b =>
                {
                    b.Property<int>("IdZamowienia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdZamowienia"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdOdbiorcy")
                        .HasColumnType("int");

                    b.Property<long>("Kontrolna")
                        .HasColumnType("bigint");

                    b.Property<double>("Koszt")
                        .HasColumnType("float");

                    b.HasKey("IdZamowienia");

                    b.HasIndex("IdOdbiorcy");

                    b.ToTable("Zamowienia");
                });

            modelBuilder.Entity("WebApplication2.Models.ProduktZamowienie", b =>
                {
                    b.HasOne("WebApplication2.Models.Produkt", "Produkt")
                        .WithMany("ProduktZamowienia")
                        .HasForeignKey("IdProduktu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Zamowienie", "Zamowienie")
                        .WithMany("ProduktZamowienia")
                        .HasForeignKey("IdZamowienia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produkt");

                    b.Navigation("Zamowienie");
                });

            modelBuilder.Entity("WebApplication2.Models.Zamowienie", b =>
                {
                    b.HasOne("WebApplication2.Models.Klient", "Klient")
                        .WithMany("Zamowienia")
                        .HasForeignKey("IdOdbiorcy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klient");
                });

            modelBuilder.Entity("WebApplication2.Models.Klient", b =>
                {
                    b.Navigation("Zamowienia");
                });

            modelBuilder.Entity("WebApplication2.Models.Produkt", b =>
                {
                    b.Navigation("ProduktZamowienia");
                });

            modelBuilder.Entity("WebApplication2.Models.Zamowienie", b =>
                {
                    b.Navigation("ProduktZamowienia");
                });
#pragma warning restore 612, 618
        }
    }
}

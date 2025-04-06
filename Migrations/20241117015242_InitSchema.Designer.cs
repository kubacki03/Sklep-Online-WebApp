﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.Models;

#nullable disable

namespace WebApplication2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241117015242_InitSchema")]
    partial class InitSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication2.Models.Klient", b =>
                {
                    b.Property<int>("IdKlienta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdKlienta"));

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

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProduktu");

                    b.ToTable("Produkty");
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

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdOdbiorcy")
                        .HasColumnType("int");

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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class ProduktZamowienie
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Zamowienie")]
        public int IdZamowienia { get; set; }
        public Zamowienie Zamowienie { get; set; }

        public int Ilosc { get; set; }

        [ForeignKey("Produkt")]
        public int IdProduktu { get; set; }
        public Produkt Produkt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Zamowienie
    {
        [Key]
        public int IdZamowienia { get; set; }
        public DateTime Data { get; set; }

        [ForeignKey("Klient")]
        public int IdOdbiorcy { get; set; }
        public Klient Klient { get; set; }

        public double Koszt { get; set; }   

        public long Kontrolna { get; set; }  

        public ICollection<ProduktZamowienie> ProduktZamowienia { get; set; }
    }
}

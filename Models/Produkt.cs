using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Produkt
    {
        [Key]
        [Required(ErrorMessage = "Nie wiem co sie dzieje")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduktu { get; set; }

        public string Opis { get; set; }
        public string Kategoria { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
        public string Nazwa { get; set; }

        [Required(ErrorMessage = "Producent jest wymagany.")]
        public string Producent { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa niż 0.")]
        [Required(ErrorMessage = "Cena jest wymagana.")]
        public double Cena { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być większa niż 0.")]
        [Required(ErrorMessage = "Ilość jest wymagana.")]
        public int Ilosc { get; set; }

      
        public ICollection<ProduktZamowienie> ProduktZamowienia { get; set; }

       
    

        public Produkt() { }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication2.Models
{
    public class Klient
    {
        [Key]
        public int IdKlienta { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
 
       public string Email { get; set; }

        [AllowNull]
        public string Login { get; set; }
        [AllowNull]
        public string Haslo { get; set; }
        public string Adres { get; set; }


        public ICollection<Zamowienie> Zamowienia { get; set; }
       
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class DailyZnizka
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Klient")]
        public int IdKlienta { get; set; }
        [ForeignKey("Produkt")]
        public int IdProduktu { get; set; }
        public double Znizka { get; set; }
        public DateOnly Data { get; set; }
    }
}

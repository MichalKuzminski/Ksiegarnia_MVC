using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksiegarnia_MVC.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        public string? Title { get; set; }

        [Display(Name = "Autor")]
        public string? Author { get; set; }

        [Display(Name = "Data zakupu")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Gatunek")]
        public string? Genre { get; set; }

        [Display(Name = "Cena")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}

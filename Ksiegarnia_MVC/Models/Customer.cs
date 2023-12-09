using System.ComponentModel.DataAnnotations;

namespace Ksiegarnia_MVC.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        // Relacja do Books
        public ICollection<Book>? Books { get; set; }
    }
}

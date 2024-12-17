using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Trif_Samuel_Lab2.Models
{
    public class Book
    {
     public int ID { get; set; }
     
     [Display(Name = "Book Title")]
     public string Title { get; set; }

        //public string Author { get; set; }

        //[Column(TypeName = "decimal(6, 2)")]
     [Column(TypeName = "decimal(6, 2)")] [Range(0.01, 500)]
     public decimal Price { get; set; }
        //[DataType(DataType.Date)]
     [DataType(DataType.Date)]
     public DateTime PublishingDate { get; set; }
     public int? PublisherID { get; set; }
     public Publisher? Publisher { get; set; }   //navigation property
     public int? AuthorID { get; set; }
    
     public Author? Author { get; set; }     //navigation property
     public ICollection<Borrowing>? Borrowings { get; set; }
     public ICollection<BookCategory>? BookCategories { get; set; }
     }
}

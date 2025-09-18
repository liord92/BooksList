using System.ComponentModel.DataAnnotations;

namespace BooksList.Models
{
    public class Book
    {
        [Required]
        public string Isbn { get; set; }
        [Required]
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Cover { get; set; }


        public Book()
        {
            Authors = new List<string>();
        }
    }
}

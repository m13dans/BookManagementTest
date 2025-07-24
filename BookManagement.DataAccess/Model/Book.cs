using System.ComponentModel.DataAnnotations;

namespace BookManagement.DataAccess.Model
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
    }

    public class CreateBookRequestModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
    }
}

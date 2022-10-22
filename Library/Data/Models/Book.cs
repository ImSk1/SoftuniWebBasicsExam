using Library.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models
{
    public class Book
    {
        public Book()
        {
            ApplicationUsersBooks = new HashSet<ApplicationUserBook>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.BOOK_TITLE_MAXLENGHT)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.BOOK_AUTHOR_MAXLENGHT)]
        public string Author { get; set; } = null!;
        [Required]
        [StringLength(ValidationConstants.BOOK_DESCRIPTION_MAXLENGHT)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), ValidationConstants.BOOK_RATING_MINVAL, ValidationConstants.BOOK_RATING_MAXVAL, ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public virtual ICollection<ApplicationUserBook> ApplicationUsersBooks  { get; set; }
    }
}

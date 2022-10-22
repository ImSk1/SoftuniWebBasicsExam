using Library.Common;
using Library.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookAddViewModel
    {
        [Required]
        [StringLength(ValidationConstants.BOOK_TITLE_MAXLENGHT, MinimumLength = ValidationConstants.BOOK_TITLE_MINLENGHT)]
        public string Title { get; set; }

        [Required]
        [StringLength(ValidationConstants.BOOK_AUTHOR_MAXLENGHT, MinimumLength = ValidationConstants.BOOK_AUTHOR_MINLENGHT)]
        public string Author { get; set; }
        [Required]
        [StringLength(ValidationConstants.BOOK_DESCRIPTION_MAXLENGHT, MinimumLength = ValidationConstants.BOOK_DESCRIPTION_MINLENGHT)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), ValidationConstants.BOOK_RATING_MINVAL, ValidationConstants.BOOK_RATING_MAXVAL, ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}

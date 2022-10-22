using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; }
    }
}

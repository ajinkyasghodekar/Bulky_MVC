using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Category Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}

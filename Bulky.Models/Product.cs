using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required] 
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Range(1,1000)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Price 1-50")]
        public double Price { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Price 50+")]
        public double Price50 { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Price 100+")]
        public double Price100 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}

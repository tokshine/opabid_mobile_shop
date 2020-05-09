using System.ComponentModel.DataAnnotations;

namespace Shivonet.MobileShop.API.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string ShortDescription { get; set; }

        [StringLength(2000)]
        public string LongDescription { get; set; }

        [StringLength(500)]
        public string AllergyInformation { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string ImageThumbnailUrl { get; set; }
        public bool IsProductOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}

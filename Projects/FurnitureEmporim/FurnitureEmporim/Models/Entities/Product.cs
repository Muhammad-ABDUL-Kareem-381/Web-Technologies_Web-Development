using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FurnitureEmporim.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        [MaxLength(100)]
        public string ImageFileName { get; set; }

        public IFormFile ImageFile { get; set; }

        public DateTime CreatedAt { get; set; }
       

    }
}

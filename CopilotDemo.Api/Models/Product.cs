using System.ComponentModel.DataAnnotations;

namespace CopilotDemo.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(3)]
        public string Currency { get; set; } = string.Empty;
    }
}
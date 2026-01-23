using System.ComponentModel.DataAnnotations;

namespace CopilotDemo.Api.Models
{
    public class ProductCreateDto
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(3)]
        public string? Currency { get; set; }
    }
}
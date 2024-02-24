using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class PlatformCreateDto
    {
        [Required]
        public string? PlatformName { get; set; }
    }
}
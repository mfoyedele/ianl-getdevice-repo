using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebApi.Binders;

namespace WebApi.Entities
{

    [ModelBinder(BinderType = typeof(PlatformModelBinder))]
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? PlatformName { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Entities
{
    public class Devices
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DeviceTypeId { get; set; }  
        [Required]  
        public string Device { get; set; }
        [Required]
        public int Time {get; set; }
        [Required]
        public int SeqNumber {get; set; }
        [Required]
        public string Data {get; set; }
    }
}
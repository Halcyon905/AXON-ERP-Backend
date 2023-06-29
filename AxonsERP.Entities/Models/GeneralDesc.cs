using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models 
{
    public class GeneralDesc 
    {
        [Required]
        public string? gdCode { get; set; }
        [Required]
        public string? desc1 { get; set; }
        [Required]
        public string? desc2 { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CompanyData.Data.DTOs
{
    public class GenerateDataDto
    {
        [Required]
        [Display(Name ="Number of Company")]
        [Range(1, 10000)]
        public int CompanyNumber { get; set; } = 100;

        [Required]
        [Display(Name = "Minimum number of Contacts")]
        [Range(1, 10000)]
        public int MinContactNumber { get; set; } = 1;

        [Required]
        [Display(Name = "Maximum number of Contacts")]
        [Range(1, 10000)]
        public int MaxContactNumber { get; set; } = 10;

        [Required]
        [Display(Name = "Minimum number of Orders")]
        [Range(1, 10000)]
        public int MinOrderNumber { get; set; } = 1;

        [Required]
        [Display(Name = "Maximum number of Orders")]
        [Range(1, 10000)]
        public int MaxOrderNumber { get; set; } = 10;

        [Required]
        [Display(Name = "Minimum price of Order")]
        [Range(1, int.MaxValue)]
        public int MinOrderPrice { get; set; } = 1;

        [Required]
        [Display(Name = "Maximum price of Order(EUR)")]
        [Range(1, int.MaxValue)]
        public int MaxOrderPrice{ get; set; } = 1000;


    }
}

using System.ComponentModel.DataAnnotations;

namespace CompanyData.Data.DTOs
{
    public class GenerateDataDTO
    {
        [Required]
        [Display(Name ="Number of Company")]
        public int CompanyNumber { get; set; } = 100;

        [Required]
        [Display(Name = "Minimum number of Contacts")]
        public int MinContactNumber { get; set; } = 1;

        [Required]
        [Display(Name = "Maximum number of Contacts")]
        public int MaxContactNumber { get; set; } = 10;

        [Required]
        [Display(Name = "Minimum number of Orders")]
        public int MinOrderNumber { get; set; } = 1;

        [Required]
        [Display(Name = "Maximum number of Orders")]
        public int MaxOrderNumber { get; set; } = 10;

        [Required]
        [Display(Name = "Minimum price of Order")]
        public int MinOrderPrice { get; set; } = 1;

        [Required]
        [Display(Name = "Maximum price of Order")]
        public int MaxOrderPrice{ get; set; } = 1000;


    }
}

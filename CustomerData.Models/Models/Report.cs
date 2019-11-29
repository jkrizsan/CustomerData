using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyData.Data.Models
{
    public class Report
    {
        public int Id { get; set; }
        [Display(Name = ("Table Name"))]
        public string TableName { get; set; }
        [Display(Name = ("Time"))]
        public DateTime DateTime { get; set; }
        [Display(Name = ("Key Values"))]
        public string KeyValues { get; set; }
        [Display(Name = ("Old Values"))]
        public string OldValues { get; set; }
        [Display(Name = ("New Values"))]
        public string NewValues { get; set; }
    }
}

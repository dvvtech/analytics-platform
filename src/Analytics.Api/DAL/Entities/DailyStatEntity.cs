using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Analytics.Api.DAL.Entities
{
    public class DailyStatEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime StatDate { get; set; }

        [MaxLength(2)]
        public string CountryCode { get; set; }

        [MaxLength(100)]
        public string OperatingSystem { get; set; }

        public int VisitsCount { get; set; }
    }
}

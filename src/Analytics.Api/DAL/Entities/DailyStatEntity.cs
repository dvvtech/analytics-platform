using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Analytics.Api.DAL.Entities
{
    public class DailyStatEntity
    {        
        public int Id { get; set; }
        
        public DateTime StatDate { get; set; }
        
        public string CountryCode { get; set; }
        
        public string OperatingSystem { get; set; }

        public int VisitsCount { get; set; }
    }
}

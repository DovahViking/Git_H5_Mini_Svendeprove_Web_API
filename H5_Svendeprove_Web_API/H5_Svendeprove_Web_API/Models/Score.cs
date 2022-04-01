using System.ComponentModel.DataAnnotations;

namespace H5_Svendeprove_Web_API.Models
{
    public class Score
    {
        [Key]
        public int id { get; set; }
        public int? highest_score { get; set; }
        public int? recent_score { get; set; }
        public DateTime last_updated { get; set; }

        public int userId { get; set; }
        public User user { get; set; }
    }
}

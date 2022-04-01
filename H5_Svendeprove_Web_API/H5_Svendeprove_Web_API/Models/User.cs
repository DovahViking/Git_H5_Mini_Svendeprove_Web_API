using System.ComponentModel.DataAnnotations;

namespace H5_Svendeprove_Web_API.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date_created { get; set; }

        public Device device { get; set; }
        public Score score { get; set; }
    }
}

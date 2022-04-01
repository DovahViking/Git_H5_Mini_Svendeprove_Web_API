using System.ComponentModel.DataAnnotations;

namespace H5_Svendeprove_Web_API.Models
{
    public class Device
    {
        [Key]
        public int id { get; set; }
        public string device_id { get; set; }
        public string platform { get; set; }
        public string version { get; set; }
        public string manufacturer { get; set; }

        public int userId { get; set; }
        public User user { get; set; }
    }
}

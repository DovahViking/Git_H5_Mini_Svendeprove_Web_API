namespace H5_Svendeprove_Web_API.DTOs
{
    public class User_DTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date_created { get; set; }
        public Device_DTO device { get; set; }
        public Score_DTO score { get; set; }
    }
}

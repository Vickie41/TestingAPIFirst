using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models.Requests
{
    public class Spouse
    {
        public string? Name_Eng { get; set; }
        public string? Name_MM { get; set; }
        public string? IDType { get; set; }
        public string? NIDType { get; set; }
        public string? NRC_Region { get; set; }
        public string? NRC_No { get; set; }
        public string? IDCard_No { get; set; }
        public DateTime? IDCard_ExpireAt { get; set; }
    }
}
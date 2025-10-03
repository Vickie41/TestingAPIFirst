using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models.Requests
{
    public class CustomerInformation
    {
        public string NameEnglish { get; set; } = string.Empty;
        public string NameMM { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string FatherNameEnglish { get; set; } = string.Empty;
        public string FatherNameMM { get; set; } = string.Empty;
        public string Marital { get; set; } = string.Empty;
        public string IDType { get; set; } = string.Empty;
        public string NIDType { get; set; } = string.Empty;
        public string NRCRegion { get; set; } = string.Empty;
        public string NRCNumber { get; set; } = string.Empty;
        public string Race { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public string IDCNumber { get; set; } = string.Empty;
        public DateTime IDCExpireAt { get; set; }
        public string Phone { get; set; } = string.Empty;
        public Address Address { get; set; } = new Address();
        public Spouse? Spouse { get; set; }
    }
}

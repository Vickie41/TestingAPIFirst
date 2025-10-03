using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models.Requests
{
    public class Address
    {
        public string AddressTypeCode { get; set; } = string.Empty;
        public string StateOrRegionCode { get; set; } = string.Empty;
        public string TownshipCode { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public string? PostalCode { get; set; }
        public string? Remark { get; set; }
    }
}
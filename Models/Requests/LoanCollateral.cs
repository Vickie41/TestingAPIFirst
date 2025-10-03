using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models.Requests
{
    public class LoanCollateral
    {
        public string CollateralType { get; set; } = string.Empty;
        public string CollateralReference { get; set; } = string.Empty;
        public decimal MarketValue { get; set; }
        public decimal ForceSaleValue { get; set; }
        public string Description { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public string TownshipCode { get; set; } = string.Empty;
    }
}

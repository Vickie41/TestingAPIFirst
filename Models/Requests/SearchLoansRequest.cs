namespace FirstTestingAPI.Models.Requests
{
    public class SearchLoansRequest
    {
        public DateTime StartDate { get; set; } = new DateTime(0001, 01, 01);
        public DateTime EndDate { get; set; } = new DateTime(3000, 09, 05);
        public int Page { get; set; } = 1;
    }
}

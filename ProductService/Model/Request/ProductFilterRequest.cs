namespace ProductService.Model.Request
{
    public class ProductFilterRequest
    {
        public string? Search { get; set; }
        public Guid? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; } = "CreatedOn"; // default column
        public string? SortDirection { get; set; } = "desc"; // asc or desc
    }
}

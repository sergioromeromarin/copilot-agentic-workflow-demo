namespace CopilotDemo.Api.Options
{
    public class ProductsOptions
    {
        public string DefaultCurrency { get; set; } = "USD";
        public int MaxNameLength { get; set; } = 50;
        public decimal MinPrice { get; set; } = 0.01M;
        public decimal MaxPrice { get; set; } = 10000M;
    }
}

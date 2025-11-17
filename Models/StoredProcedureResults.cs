using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBFirstApproach.Context;
using DBFirstApproach.Models;

namespace DBFirstApproach.Models
{
    public class ProductDetailsResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? MarkaName { get; set; }
        public string? MarkaDescription { get; set; }
    }

    public class ProductFeaturesResult
    {
        public long Id { get; set; }
        public decimal Discount { get; set; }
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
    }

    public class ProductCommentsResult
    {
        public long Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Username { get; set; }
    }

    public class UserOrderHistoryResult
    {
        public long OrderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ProductCount { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}

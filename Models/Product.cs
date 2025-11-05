using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproach.Models;

[Index("MarkaId", Name = "IX_Products_MarkaId")]
public partial class Product
{
    [Key]
    public long Id { get; set; }

    [StringLength(128)]
    public string Name { get; set; } = null!;

    [StringLength(256)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public long MarkaId { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("MarkaId")]
    [InverseProperty("Products")]
    public virtual Marka Marka { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductFeature> ProductFeatures { get; set; } = new List<ProductFeature>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproach.Models;

[Index("ColorId", Name = "IX_ProductFeatures_ColorId")]
[Index("ProductId", Name = "IX_ProductFeatures_ProductId")]
[Index("SizeId", Name = "IX_ProductFeatures_SizeId")]
public partial class ProductFeature
{
    [Key]
    public long Id { get; set; }

    [Column(TypeName = "money")]
    public decimal Discount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public long ProductId { get; set; }

    public long ColorId { get; set; }

    public long SizeId { get; set; }

    [ForeignKey("ColorId")]
    [InverseProperty("ProductFeatures")]
    public virtual Color Color { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductFeatures")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("SizeId")]
    [InverseProperty("ProductFeatures")]
    public virtual Size Size { get; set; } = null!;
}

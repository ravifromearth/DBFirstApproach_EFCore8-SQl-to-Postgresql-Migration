using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproach.Models;

[Index("OrderId", Name = "IX_OrderProducts_OrderId")]
[Index("ProductId", Name = "IX_OrderProducts_ProductId")]
public partial class OrderProduct
{
    [Key]
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderProducts")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderProducts")]
    public virtual Product Product { get; set; } = null!;
}

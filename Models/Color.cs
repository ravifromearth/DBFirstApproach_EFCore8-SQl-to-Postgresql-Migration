using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproach.Models;

public partial class Color
{
    [Key]
    public long Id { get; set; }

    [StringLength(128)]
    public string Name { get; set; } = null!;

    [StringLength(256)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Color")]
    public virtual ICollection<ProductFeature> ProductFeatures { get; set; } = new List<ProductFeature>();
}

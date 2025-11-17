using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproach.Models;

public partial class VwActiveProducts
{
    [Key]
    public long Id { get; set; }

    [StringLength(128)]
    public string? Name { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public long MarkaId { get; set; }

    [StringLength(128)]
    public string? MarkaName { get; set; }
}

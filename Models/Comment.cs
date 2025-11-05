using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproach.Models;

[Index("ProductId", Name = "IX_Comments_ProductId")]
[Index("UserId", Name = "IX_Comments_UserId")]
public partial class Comment
{
    [Key]
    public long Id { get; set; }

    [StringLength(128)]
    public string Content { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public long ProductId { get; set; }

    public long UserId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Comments")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User User { get; set; } = null!;
}

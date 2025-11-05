using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproach.Models;

[Index("UserId", Name = "IX_Sessions_UserId")]
public partial class Session
{
    [Key]
    public long Id { get; set; }

    [StringLength(256)]
    [Unicode(false)]
    public string Token { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? Expires { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public long UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Sessions")]
    public virtual User User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class TblBranding
{
    public int BrandingId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Type { get; set; }

    public int? ClientId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual TblClient? Client { get; set; }

    public virtual ICollection<TblProductbranding> TblProductbrandings { get; set; } = new List<TblProductbranding>();
}

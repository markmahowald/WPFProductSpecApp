using System;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class TblProductbranding
{
    public int ProductId { get; set; }

    public int BrandingId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual TblBranding Branding { get; set; } = null!;

    public virtual TblProduct Product { get; set; } = null!;
}

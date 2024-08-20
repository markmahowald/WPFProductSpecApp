using System;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class TblProductmaterial
{
    public int ProductId { get; set; }

    public int MaterialId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual TblMaterial Material { get; set; } = null!;

    public virtual TblProduct Product { get; set; } = null!;
}

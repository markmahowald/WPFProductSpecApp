using System;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class TblProductmold
{
    public int ProductId { get; set; }

    public int MoldId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual TblMold Mold { get; set; } = null!;

    public virtual TblProduct Product { get; set; } = null!;
}

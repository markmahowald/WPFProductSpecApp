using System;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class TblMold
{
    public int MoldId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<TblProductmold> TblProductmolds { get; set; } = new List<TblProductmold>();
}

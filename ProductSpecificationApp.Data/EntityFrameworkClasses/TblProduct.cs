using System;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Height { get; set; }

    public decimal? Width { get; set; }

    public string? Unit { get; set; }

    public string? Sku { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<TblProductbranding> TblProductbrandings { get; set; } = new List<TblProductbranding>();

    public virtual ICollection<TblProductmaterial> TblProductmaterials { get; set; } = new List<TblProductmaterial>();

    public virtual ICollection<TblProductmold> TblProductmolds { get; set; } = new List<TblProductmold>();

}

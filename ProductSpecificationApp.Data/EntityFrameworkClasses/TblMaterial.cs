using System;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class TblMaterial
{
    public int MaterialId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Height { get; set; }

    public decimal? Width { get; set; }

    public string? Unit { get; set; }

    public string? Sku { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<TblProductmaterial> TblProductmaterials { get; set; } = new List<TblProductmaterial>();

}

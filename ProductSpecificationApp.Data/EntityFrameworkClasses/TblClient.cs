using System;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class TblClient
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<TblBranding> TblBrandings { get; set; } = new List<TblBranding>();

}

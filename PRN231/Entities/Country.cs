using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class Country : BaseEntity
{

    public string? CountryName { get; set; }

    public int RegionId { get; set; }

    public virtual ICollection<Location> Locations { get; } = new List<Location>();

    public virtual Region Region { get; set; } = null!;
}

﻿using System;
using System.Collections.Generic;

namespace PRN231.Models;

public partial class Region : BaseEntity
{

    public string? RegionName { get; set; }

    //public virtual ICollection<Country> Countries { get; } = new List<Country>();
}

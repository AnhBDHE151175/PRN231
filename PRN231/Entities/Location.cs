using System;
using System.Collections.Generic;

namespace PRN231.Entities;

public partial class Location : BaseEntity
{

    public string? StreetAddress { get; set; }

    public string? PostalCode { get; set; }

    public string City { get; set; } = null!;

    public string? StateProvince { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; } = new List<Department>();
}

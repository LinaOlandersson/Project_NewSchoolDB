using System;
using System.Collections.Generic;

namespace NewSchoolDB.Models;

public partial class Profession
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

using System;
using System.Collections.Generic;

namespace NewSchoolDB.Models;

public partial class Scale
{
    public int Id { get; set; }

    public string? Mark { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}

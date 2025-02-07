using System;
using System.Collections.Generic;

namespace NewSchoolDB.Models;

public partial class Class
{
    public int Id { get; set; }

    public string? ClassName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

}

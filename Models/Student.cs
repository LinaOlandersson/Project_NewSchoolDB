using System;
using System.Collections.Generic;

namespace NewSchoolDB.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Ssn { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? ClassesId { get; set; }

    public virtual Class? Classes { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}

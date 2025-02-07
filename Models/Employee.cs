using System;
using System.Collections.Generic;

namespace NewSchoolDB.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateEmployed { get; set; }

    public decimal? Salary { get; set; }

    public int? DepartmentId { get; set; }

    public int? ProfessionsId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Profession? Professions { get; set; }
}

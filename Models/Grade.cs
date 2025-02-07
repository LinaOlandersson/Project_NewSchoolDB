using System;
using System.Collections.Generic;

namespace NewSchoolDB.Models;

public partial class Grade
{
    public int Id { get; set; }

    public DateOnly? GradeSet { get; set; }

    public int? StudentsId { get; set; }

    public int? SubjectsId { get; set; }

    public int? EmployeesId { get; set; }

    public int? ScalesId { get; set; }

    public virtual Employee? Employees { get; set; }

    public virtual Scale? Scales { get; set; }

    public virtual Student? Students { get; set; }

    public virtual Subject? Subjects { get; set; }
}

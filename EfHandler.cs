using Microsoft.EntityFrameworkCore;
using NewSchoolDB.Data;

namespace NewSchoolDB
{
    public class EfHandler
    {
        // Method for selecting all teachers, grouping and counting them by department.
        public static void TeachersPerDepartment()
        {
            using (NewSchoolDbContext context = new NewSchoolDbContext())
            {
                var result = context.Employees
                    .Where(e => e.ProfessionsId == 1)
                    .Join(
                        context.Departments,
                        e => e.DepartmentId,
                        d => d.Id,
                        (e, d) => new
                        {
                            Employee = e.Id,
                            Department = d.DepartmentName,
                        })
                    .GroupBy(d => d.Department)
                    .Select(group => new
                    {
                        Department = group.Key,
                        Count = group.Count()
                    })
                    .ToList();

                Console.WriteLine("Number of teachers per department:\n");
                foreach (var group in result)
                {
                    Console.WriteLine($"{group.Department, -23}{group.Count}");
                }
            }
        }

        // Method for presenting student name, SSN and class.
        public static void StudentInfo()
        {
            using (NewSchoolDbContext context = new NewSchoolDbContext())
            {
                var result = context.Students
                    .Include(s => s.Classes)
                    .Select(s => new
                    {
                        StudentName = s.FirstName + " " + s.LastName,
                        SSN = s.Ssn,
                        Class = s.Classes.ClassName
                    });

                Console.WriteLine($"{"Student", -23}{"SSN", -23}{"Class", -23}\n");
                foreach (var student in result)
                {
                    Console.WriteLine($"{student.StudentName, -23}{student.SSN, -23}{student.Class, -23}");
                }
            }
        }

        // Method for presenting courses (subjects) that are active. 
        public static void ActiveCourses()
        {
            using (NewSchoolDbContext context = new NewSchoolDbContext())
            {
                var result = context.Subjects
                    .Where(s => s.IsActive == true);

                Console.WriteLine("The following subjects are active at the moment:\n");
                foreach (var subject in result)
                {
                    Console.WriteLine(subject.SubjectName);
                }
            }
        }
    }
}

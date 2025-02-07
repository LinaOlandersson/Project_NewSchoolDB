using Microsoft.Data.SqlClient;

namespace NewSchoolDB
{
    public class AdoHandler
    {
        private static readonly string _connectionString = @"Data Source = localhost;Database = NewSchoolDB;Integrated Security = true;Trust Server Certificate = true;";

        
        // General method for executing a SQL query and printing it's result.
        // The result of the query is stored in a list.
        private static List<string> ExecuteQuery(string query)
        {
            Console.Clear();
            List<string> queryList = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No results found");
                        }
                        else
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader.GetName(i),-23}");
                            }
                            Console.WriteLine("\n");

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var value = reader[i];

                                    if (value is decimal) //If decimal, show two decimals.
                                    {
                                        value = Math.Round((decimal)value, 2);
                                    }
                                    else if (value is DateTime) //If DateTime, hide time.
                                    {
                                        value = ((DateTime)value).ToString("yyyy-MM-dd");
                                    }

                                    Console.Write($"{value, -23}");
                                    queryList.Add(reader[0].ToString());
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            return queryList;
        }

        // A method for getting user input regarding choices in lists.
        private static string ChooseFromQueryList(List<string> list, string choice)
        {
            Console.Clear();
            int counter = 1;
            foreach (var item in list)
            {
                Console.WriteLine($"{counter}. {item}");
                counter++;
            }
            Console.WriteLine($"\nChoose {choice}:");
            int choiceAsInt = Format.Choice(list.Count());
            string choiceAsString = list[choiceAsInt - 1];
            Console.WriteLine();
            return choiceAsString;
        }

        // Query to be used with method "ExecuteQuery".
        public static void EmployeeInfo()
        {
            string query = @"SELECT
                FirstName + ' ' + LastName AS 'Name',
                Professions.Title,
                DATEDIFF(YEAR, DateEmployed, GETDATE()) AS 'Years employed'
                FROM Employees
                JOIN Professions ON Employees.Professions_ID = Professions.ID";
            ExecuteQuery(query);
        }

        // Query to be used with method "ExecuteQuery".
        public static void GradeInfo()
        {
            Console.WriteLine("Enter student's ID:");

            int studentId;
            while (!int.TryParse(Console.ReadLine(), out studentId))
            {
                Console.WriteLine("Wrong input. Enter student's ID:");
            }

            string query = @$"SELECT 
                Students.FirstName + ' ' + Students.LastName AS 'Student',
                Subjects.SubjectName AS 'Subject',
                Scales.Mark,
                Employees.FirstName + ' ' + Employees.LastName AS 'Teacher',
                Grades.GradeSet AS 'Date'
                FROM Grades
                JOIN Subjects ON Grades.Subjects_ID = Subjects.ID
                JOIN Employees ON Grades.Employees_ID = Employees.ID
                JOIN Scales ON Grades.Scales_ID = Scales.ID
                JOIN Students ON Grades.Students_ID = Students.ID
                WHERE Students.ID = {studentId}";
            ExecuteQuery(query);
        }

        // Query to be used with method "ExecuteQuery". Student ID is user input.
        // This time the query is a Stored Procedure.
        public static void GetStudentById()
        {
            int studentId;
            Console.WriteLine("Enter student's ID:");
            while (!int.TryParse(Console.ReadLine(), out studentId))
            {
                Console.WriteLine("Wrong input. Enter student's ID:");
            }
            string query = $@"EXEC GetStudentById @StudentId = {studentId}";
            ExecuteQuery(query);
        }

        // Query to be used with method "ExecuteQuery".
        public static void SalaryPerDepartment()
        {
            string query = @"SELECT
                Departments.DepartmentName AS 'Department',
                SUM(Employees.Salary) AS 'Salary expenses per month'
                FROM Employees
                JOIN Departments ON Employees.Department_ID = Departments.ID
                GROUP BY DepartmentName";
            ExecuteQuery(query);
        }

        // Query to be used with method "ExecuteQuery".
        public static void AverageSalaryPerDepartment()
        {
            string query = @"SELECT
                Departments.DepartmentName AS 'Department',
                AVG(ROUND(Employees.Salary, 2)) AS 'Average salary'
                FROM Employees
                JOIN Departments ON Employees.Department_ID = Departments.ID
                GROUP BY DepartmentName";
            ExecuteQuery(query);
        }

        // The method takes input regarding new employee and adds to database.   
        public static void AddEmployee()
        {
            Console.WriteLine("New employee\n");
            string? firstName = null;
            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Enter first name:");
                firstName = Console.ReadLine();
            }
            firstName = char.ToUpper(firstName[0]) + firstName.Substring(1).ToLower();
            
            string? lastName = null;
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Enter last name:");
                lastName = Console.ReadLine();
            }
            lastName = char.ToUpper(lastName[0]) + lastName.Substring(1).ToLower();

            string professionsQuery = "SELECT Title FROM Professions";
            List <string> professionsList = ExecuteQuery(professionsQuery);
            string title = ChooseFromQueryList(professionsList, "title");
            
            string departmentsQuery = "SELECT DepartmentName FROM Departments";
            List<string> departmentsList = ExecuteQuery(departmentsQuery);
            string department = ChooseFromQueryList(departmentsList, "department");

            Console.Clear();
            Console.WriteLine("Date of employment\n\nEnter date (yyyy-mm-dd):");
            DateOnly date = new DateOnly();

            while (!DateOnly.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Wrong input. Enter date:");
            }

            Console.Clear();
            Console.WriteLine("Enter monthly salary:");
            decimal salary;

            while (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Wrong input. Enter salary:");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @$"DECLARE @Department_ID INT
                    SELECT @Department_ID = ID
                    FROM Departments
                    WHERE DepartmentName = @DepartmentName
                    DECLARE @Professions_ID INT
                    SELECT @Professions_ID = ID
                    FROM Professions
                    WHERE Title = @Title
                    INSERT INTO Employees(FirstName, LastName, DateEmployed, Salary, Department_ID, Professions_ID) VALUES
                    (@FirstName, @LastName, @DateEmployed, @Salary, @Department_ID, @Professions_ID)";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@DateEmployed", date);
                        command.Parameters.AddWithValue("@Salary", salary);
                        command.Parameters.AddWithValue("@DepartmentName", department);
                        command.Parameters.AddWithValue("@Title", title);

                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.Clear();
                        Console.WriteLine($"{firstName} {lastName} added to database as {title}/{department}\n" +
                            $"Monthly salary: {salary} SEK.\nDate of employment: {date}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // The method takes input regarding grades and adds to database.
        // The insert statement is regulated by a transaction.
        public static void AddGrade()
        {
            string studentsQuery = "SELECT FirstName + ' ' + LastName AS Student FROM Students";
            List<string> studentsList = ExecuteQuery(studentsQuery);
            string student = ChooseFromQueryList(studentsList, "student");

            string subjectsQuery = "SELECT SubjectName FROM Subjects";
            List<string> subjectsList = ExecuteQuery(subjectsQuery);
            string subject = ChooseFromQueryList(subjectsList, "subject");

            string scalesQuery = "SELECT Mark FROM Scales";
            List<string> scalesList = ExecuteQuery(scalesQuery);
            string mark = ChooseFromQueryList(scalesList, "mark");

            string teachersQuery = "SELECT FirstName + ' ' + LastName FROM Employees" +
                " WHERE Professions_ID = 1";
            List<string> teachersList = ExecuteQuery(teachersQuery);
            string teacher = ChooseFromQueryList(teachersList, "teacher");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $@"BEGIN TRY
                        DECLARE @Students_ID INT
                        SELECT @Students_ID = ID FROM Students WHERE FirstName + ' ' + LastName = @Student
                        DECLARE @Subjects_ID INT
                        SELECT @Subjects_ID = ID FROM Subjects WHERE SubjectName = @Subject
                        DECLARE @Scales_ID INT
                        SELECT @Scales_ID = ID FROM Scales WHERE Mark = @Mark
                        DECLARE @Employees_ID INT
                        SELECT @Employees_ID = ID FROM Employees WHERE FirstName + ' ' + LastName = @Teacher
                        BEGIN TRANSACTION
                        INSERT INTO Grades (Students_ID, Subjects_ID, Scales_ID, Employees_ID, GradeSet)
                        VALUES (@Students_ID, @Subjects_ID, @Scales_ID, @Employees_ID, GETDATE())
                        COMMIT
                        PRINT 'Grade successfully set'
                    END TRY
                    BEGIN CATCH
                        ROLLBACK
                        PRINT 'Grade set rolled back'
                    END CATCH";
                    
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Student", student);
                    command.Parameters.AddWithValue("@Subject", subject);
                    command.Parameters.AddWithValue("@Mark", mark);
                    command.Parameters.AddWithValue("@Teacher", teacher);

                    connection.Open();
                    command.ExecuteNonQuery();

                    Console.Clear();
                    Console.WriteLine($"{student} recieved {mark} in {subject}\n" +
                        $"Grade set by {teacher}");
                }
            }
        }
    }
}

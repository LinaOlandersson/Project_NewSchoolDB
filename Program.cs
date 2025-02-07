namespace NewSchoolDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Presenting the different menus.
            
            List<string> headMenu = new List<string>
            {
                "[1] Employee info",
                "[2] Student info",
                "[3] Courses info",
                "[4] Economics info",
                "[5] End program"
            };

            List<string> employeeMenu = new List<string>
            {
                 "[1] Show number of teachers per department",
                 "[2] Show employee info",
                 "[3] Add employee",
                 "[4] Return to menu"
            };

            List<string> studentMenu = new List<string>
            {
                "[1] Show student info",
                "[2] Search student by ID",
                "[3] Search grades by student ID",
                "[4] Add grades to student",
                "[5] Return to menu"
            };

            List<string> coursesMenu = new List<string>
            {
                "[1] Show active courses",
                "[2] Return to menu"
            };

            List<string> economicsMenu = new List<string>
            {
                "[1] Monthly salary expenses per department",
                "[2] Average salary per department",
                "[3] Return to menu"
            };

            bool go = true;
            while (go)
            {
                Console.Clear();
                Console.WriteLine("--- SCHOOL ADMINISTRATIVE SYSTEM ---");
                Console.WriteLine();
                foreach (string s in headMenu)
                {
                    Console.WriteLine(s);
                }
                int choiceHeadMenu = Format.Choice(headMenu.Count());
                Console.Clear();

                // Navigating through menus and calling on relevant methods.
                switch (choiceHeadMenu)
                {
                    case 1:
                        foreach (string s in employeeMenu)
                        {
                            Console.WriteLine(s);
                        }
                        int choiceEmployeeMenu = Format.Choice(employeeMenu.Count());

                        switch (choiceEmployeeMenu)
                        {
                            case 1:
                                Console.Clear(); 
                                EfHandler.TeachersPerDepartment();
                                Format.ReturnToMenu();
                                break;
                            case 2:
                                Console.Clear(); 
                                AdoHandler.EmployeeInfo();
                                Format.ReturnToMenu();
                                break;
                            case 3:
                                Console.Clear(); 
                                AdoHandler.AddEmployee();
                                Format.ReturnToMenu();
                                break;
                            case 4: break;
                            default: break;
                        }
                        break;
                    case 2:
                        foreach (string s in studentMenu)
                        {
                            Console.WriteLine(s);
                        }
                        int choiceStudentMenu = Format.Choice(studentMenu.Count());

                        switch (choiceStudentMenu)
                        {
                            case 1:
                                Console.Clear(); 
                                EfHandler.StudentInfo();
                                Format.ReturnToMenu();
                                break;
                            case 2:
                                Console.Clear(); 
                                AdoHandler.GetStudentById();
                                Format.ReturnToMenu();
                                break;
                            case 3:
                                Console.Clear(); 
                                AdoHandler.GradeInfo();
                                Format.ReturnToMenu();
                                break;
                            case 4:
                                Console.Clear(); 
                                AdoHandler.AddGrade();
                                Format.ReturnToMenu();
                                break;
                            case 5: break;
                            default: break;
                        }
                        break;
                    case 3:
                        foreach (string s in coursesMenu)
                        {
                            Console.WriteLine(s);
                        }
                        int choiceCoursesMenu = Format.Choice(coursesMenu.Count());
                        switch (choiceCoursesMenu)
                        {
                            case 1:
                                Console.Clear(); 
                                EfHandler.ActiveCourses();
                                Format.ReturnToMenu();
                                break;
                            case 2: break;
                            default: break;
                        }
                        break;
                    case 4:
                        foreach (string s in economicsMenu)
                        {
                            Console.WriteLine(s);
                        }
                        int choiceEconomicsMenu = Format.Choice(economicsMenu.Count());
                        switch (choiceEconomicsMenu)
                        {
                            case 1:
                                Console.Clear(); 
                                AdoHandler.SalaryPerDepartment();
                                Format.ReturnToMenu();
                                break;
                            case 2:
                                Console.Clear(); 
                                AdoHandler.AverageSalaryPerDepartment();
                                Format.ReturnToMenu();
                                break;
                            case 3: break;
                            default: break;
                        }
                        break;
                    case 5:
                        go = false;
                        break;
                    default: break;
                }
            }
        }
    }
}

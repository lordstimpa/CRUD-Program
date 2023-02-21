using _16_db_csharp;

namespace dbcsharp;
class Program
{
    static void Main(string[] args)
    {
        DisplayMenu();
    }

    public static void DisplayMenu()
    {
        string input;
        bool repeat = true;

        do
        {
            Console.Clear();

            DisplayAscii();
            Console.WriteLine("\n Welcome to the database! \n Kindly pick an option from the menu below:\n");
            Console.WriteLine(" 1. List students");
            Console.WriteLine(" 2. List courses");
            Console.WriteLine(" 3. Create student");
            Console.WriteLine(" 4. Create course");
            Console.WriteLine(" 5. Change password");
            Console.WriteLine(" 6. Edit course");
            Console.WriteLine(" 7. Delete course");
            Console.WriteLine(" E. Exit");

            Console.Write("\n ---> ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // List all students
                    ListStudents();
                    break;

                case "2":
                    // List all courses
                    ListCourses();
                    break;

                case "3":
                    // Create a new student
                    AddStudent();
                    break;

                case "4":
                    // Create a new course
                    AddCourse();
                    break;

                case "5":
                    // Change password on a student
                    ChangePassword();
                    break;

                case "6":
                    // Edit exisiting course
                    EditCourse();
                    break;

                case "7":
                    // Delete existing course
                    DeleteCourse();
                    break;

                case "E":
                case "e":
                    // Exit program
                    Console.Clear();
                    Console.Write("\n Thanks for using the database.");
                    repeat = false;
                    break;

                default:
                    // Error handler
                    Console.Clear();
                    Console.WriteLine("\n Kindly pick an option from the menu.");
                    Console.Write("\n Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }
        while (repeat);
    }

    static void DisplayAscii()
    {
        string asciiLayer0 = "          ,..........   ..........,";
        string asciiLayer1 = "      ,..,'          '.'          ',..,";
        string asciiLayer2 = "     ,' ,'            :            ', '";
        string asciiLayer3 = "    ,' ,'             :             ', '";
        string asciiLayer4 = "   ,' ,'              :              ', ',";
        string asciiLayer5 = "  ,' ,'............., : ,.............', ',";
        string asciiLayer6 = " ,'  '............   '.'   ............'  ',";
        string asciiLayer7 = "  '''''''''''''''''';''';''''''''''''''''''";
        string asciiLayer8 = "                     '''";

        string[] asciiArt = { asciiLayer0, asciiLayer1, asciiLayer2, asciiLayer3, asciiLayer4, asciiLayer5, asciiLayer6, asciiLayer7, asciiLayer8};

        foreach (string layer in asciiArt)
        {
            Console.WriteLine(layer);
        }
    }

    static void ListStudents()
    {
        Console.Clear();
        List<StudentModel> users = PostgresDataAccess.LoadStudents();
        foreach (StudentModel user in users)
        {
            Console.WriteLine("\n ----------------------------------");
            Console.WriteLine($" ID: {user.Id}\n First name: {user.First_name}\n Last name: {user.Last_name}\n Email: {user.Email}\n Age: {user.Age}\n Password: {user.Password}");
        }
        Console.Write("\n Press any key to continue.");
        Console.ReadLine();
    }

    static void ListCourses()
    {
        Console.Clear();
        List<CourseModel> courses = PostgresDataAccess.LoadCourses();
        foreach (CourseModel course in courses)
        {
            Console.WriteLine("\n ----------------------------------");
            Console.WriteLine($" ID: {course.Id}\n Course: {course.Course_name}\n Points: {course.Points}\n Start date: {course.Start_date}\n End date: {course.End_date}");
        }
        Console.Write("\n Press any key to continue.");
        Console.ReadLine();
    }

    static void AddStudent()
    {
        bool repeat = true;
        do
        {
            Console.Clear();
            Console.WriteLine("\n ---Add new student---");
            Console.Write(" First name: ");
            string first_name = Console.ReadLine();

            Console.Write(" Last name: ");
            string last_name = Console.ReadLine();

            Console.Write(" Email: ");
            string email = Console.ReadLine();

            Console.Write(" Age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write(" Password: ");
            string password = Console.ReadLine();

            if (first_name != null && last_name != null && email != null && age > 0 && password != null)
            {
                StudentModel newStudent = new StudentModel
                {
                    First_name = first_name,
                    Last_name = last_name,
                    Email = email,
                    Age = age,
                    Password = password

                };
                PostgresDataAccess.CreateStudent(newStudent);

                Console.WriteLine("\n A new student was successfully added to the database.");
                Console.Write("\n Press any key to return.");
                Console.ReadLine();
                repeat = false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Kindly fill all the required fields.");
                Console.Write("\n Press any key to continue.");
                Console.ReadLine();
            }

        } while (repeat);
    }

    static void AddCourse()
    {
        bool repeat = true;
        do
        {
            Console.Clear();
            Console.WriteLine("\n ---Add new course---");
            Console.Write(" Course name: ");
            string Course_Name = Console.ReadLine();

            Console.Write(" Course points: ");
            int Points = Convert.ToInt32(Console.ReadLine());

            Console.Write(" Start date: ");
            string Start_Date = Console.ReadLine();

            Console.Write(" End date: ");
            string End_Date = Console.ReadLine();

            if (Course_Name != null && Points > 0 && Start_Date != null && End_Date != null)
            {
                CourseModel newCourse = new CourseModel
                {
                    Course_name = Course_Name,
                    Points = Points,
                    Start_date = Start_Date,
                    End_date = End_Date
                };

                PostgresDataAccess.CreateCourse(newCourse);

                Console.WriteLine("\n A new course was successfully added to the database.");
                Console.Write("\n Press any key to return.");
                Console.ReadLine();
                repeat = false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Please fill all the required fields.");
                Console.Write("\n Press any key to continue.");
                Console.ReadLine();
            }

        } while (repeat);
    }

    static void ChangePassword()
    {
        List<StudentModel> students = PostgresDataAccess.LoadStudents();
        bool successParse;

        do
        {
            Console.Clear();
            Console.WriteLine("\n ---Change password---");
            Console.WriteLine("\n Kindly pick a student to change the password for below: \n");

            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {students[i].First_name} {students[i].Last_name}");
            }
            Console.WriteLine("\n Enter 'E' to return to the menu.");

            Console.Write("\n ---> ");
            string input = Console.ReadLine();
            successParse = int.TryParse(input, out int inputInt);

            if (successParse == true)
            {
                bool repeatPasswordInput = true;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"\n Current password: {students[inputInt - 1].Password}");

                    Console.Write("\n New password: ");
                    string newPassword = Console.ReadLine();

                    if (newPassword != null)
                    {
                        PostgresDataAccess.ChangePassword(newPassword, students[inputInt - 1].Id);
                        Console.WriteLine("\n Password successfully updated.");
                        Console.WriteLine("\n Press any key to continue.");
                        Console.ReadLine();
                        repeatPasswordInput = false;
                    }
                    else
                    {
                        Console.WriteLine("\n Password cannot be empty.");
                        Console.WriteLine("\n Press any key to try again.");
                        Console.ReadLine();
                    }

                } while (repeatPasswordInput);    
            }
            else if (input == "e" || input == "E")
            {
                successParse = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Kindly pick a student listed.");
                Console.Write("\n Press any key to continue.");
                Console.ReadLine();
            }

        } while (successParse == false);
    }

    static void EditCourse()
    {
        List<CourseModel> course = PostgresDataAccess.LoadCourses();
        bool successParse;

        do
        {
            Console.Clear();
            Console.WriteLine("\n ---Edit course---");
            Console.WriteLine("\n Kindly pick a course to edit: ");

            for (int i = 0; i < course.Count; i++)
            {
                Console.WriteLine($"\n {i + 1}. Course: {course[i].Course_name}\n    Points: {course[i].Points}\n    Start date: {course[i].Start_date}\n    End date: {course[i].End_date}");
            }
            Console.WriteLine("\n Enter 'E' to return to the menu.");

            Console.Write("\n ---> ");
            string input = Console.ReadLine();
            successParse = int.TryParse(input, out int inputInt);

            if (successParse == true)
            {
                bool repeatCourseInput = true;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"\n Course: {course[inputInt - 1].Course_name}\n Points: {course[inputInt - 1].Points}\n Start date: {course[inputInt - 1].Start_date}\n End date: {course[inputInt - 1].End_date}");

                    Console.Write("\n Course name: ");
                    string courseName = Console.ReadLine();

                    Console.Write("\n Course points: ");
                    int coursePoints = Convert.ToInt32(Console.ReadLine());

                    Console.Write("\n Course start date: ");
                    string courseStart = Console.ReadLine();

                    Console.Write("\n Course end date: ");
                    string courseEnd = Console.ReadLine();

                    if (courseName != null && coursePoints > 0 && courseStart != null && courseEnd != null)
                    {
                        PostgresDataAccess.EditCourse(courseName, coursePoints, courseStart, courseEnd, course[inputInt - 1].Id);
                        Console.Clear();
                        Console.WriteLine("\n Course successfully updated.");
                        Console.WriteLine("\n Press any key to continue.");
                        Console.ReadLine();
                        repeatCourseInput = false;
                    }
                    else
                    {
                        Console.WriteLine("\n Please fill all the required fields.");
                        Console.WriteLine("\n Press any key to try again.");
                        Console.ReadLine();
                    }
                } while (repeatCourseInput);
              
            }
            else if (input == "e" || input == "E")
            {
                successParse = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Kindly pick a course listed.");
                Console.Write("\n Press any key to continue.");
                Console.ReadLine();
            }

        } while (successParse == false);
    }

    static void DeleteCourse()
    {
        List<CourseModel> course = PostgresDataAccess.LoadCourses();
        bool successParse;

        do
        {
            Console.Clear();
            Console.WriteLine("\n ---Delete course---");
            Console.WriteLine("\n Kindly pick a course to delete: ");

            for (int i = 0; i < course.Count; i++)
            {
                Console.WriteLine($"\n {i + 1}. Course: {course[i].Course_name}\n    Points: {course[i].Points}\n    Start date: {course[i].Start_date}\n    End date: {course[i].End_date}");
            }
            Console.WriteLine("\n Enter 'E' to return to the menu.");

            Console.Write("\n ---> ");
            string input = Console.ReadLine();
            successParse = int.TryParse(input, out int inputInt);

            if (successParse == true)
            {
                PostgresDataAccess.DeleteCourse(course[inputInt - 1].Id);
                Console.Clear();
                Console.WriteLine($"\n Course: {course[inputInt - 1].Course_name} has been successfully deleted.");
                Console.WriteLine("\n Press any key to continue.");
                Console.ReadLine();
            }
            else if (input == "e" || input == "E")
            {
                successParse = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Kindly pick a course listed.");
                Console.Write("\n Press any key to continue.");
                Console.ReadLine();
            }

        } while (successParse == false);
    }
}


using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_db_csharp
{
    public class PostgresDataAccess
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static List<StudentModel> LoadStudents()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<StudentModel>("SELECT * FROM sda_student", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<CourseModel> LoadCourses()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<CourseModel>("SELECT * FROM sda_course", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void CreateStudent(StudentModel newStudent)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"INSERT INTO sda_student (first_name, last_name, email, age, password) VALUES (@First_name, @Last_name, @Email, @Age, @Password)", newStudent);
            }
        }
        
        public static void CreateCourse(CourseModel newCourse)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"INSERT INTO sda_course (name, points, start_date, end_date) VALUES (@Name, @Points, @Start_date, @End_date)", newCourse);
            }
        }
        
        public static void ChangePassword(string newPassword, int id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE sda_student SET password = '{newPassword}' WHERE id = '{id}'", new DynamicParameters());
            }
        }
        
        public static void EditCourse(string Name, int Points, string Start, string End, int id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE sda_course SET course_name = '{Name}', points = '{Points}', start_date = '{Start}', end_date = '{End}' WHERE id = '{id}'", new DynamicParameters());
            }
        }

        public static void DeleteCourse(int id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"DELETE FROM sda_course WHERE id = {id}", new DynamicParameters());
            }
        }
    }
}

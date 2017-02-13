using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string connectionString="Data Source=.\\SQLEXPRESS;AttachDbFilename=\"c:\\users\\user1\\documents\\visual studio 2010\\Projects\\ConsoleApplication1\\ConsoleApplication1\\Database1.mdf\";Integrated Security=True;User Instance=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //CreateAllTables(connection);
            //InsertAllData(connection);

            int id = GetStudentId("John", "Doe", connection);

            //PrintTable("Students", connection);



            connection.Close();
        }

        private static int GetStudentId(string firstName, string lastName, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = string.Format("SELECT Id FROM Students WHERE FirstName='{0}' AND LastName='{1}';", firstName, lastName);
            /*SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = reader.GetInt32(0);      
            reader.Close();
            return id;*/
            return (int)command.ExecuteScalar();
        }

        private static void InsertAllData(SqlConnection connection)
        {
            InsertStudent("John", "Doe", connection);
            InsertTeacher("Eidan", "Cohen", "Computer Science", connection);
            InsertGrade(1, 1, 90, connection);
        }

        private static void PrintTable(string tableName, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = string.Format("SELECT * FROM {0};", tableName);
            SqlDataReader reader = command.ExecuteReader();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader.GetName(i) + "\t");
            }

            Console.WriteLine();
            
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader.GetValue(i).ToString() + "\t");
                }
                Console.WriteLine();
            }

            reader.Close();
        }

        private static void InsertGrade(int studentId, int teacherId, int grade, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = string.Format("INSERT INTO Grades VALUES('{0}','{1}','{2}');", studentId, teacherId, grade);
            command.ExecuteNonQuery();
          
        }

        private static void InsertTeacher(string firstName, string lastName, string profession, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = string.Format("INSERT INTO Teachers VALUES('{0}','{1}','{2}');",firstName,lastName,profession);
            command.ExecuteNonQuery();
        }

        private static void InsertStudent(string firstName,string lastName,SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = string.Format("INSERT INTO Students VALUES ('{0}','{1}');",firstName,lastName);
            command.ExecuteNonQuery();
        }

        private static void CreateAllTables(SqlConnection connection)
        {
            CreateStudentsTable(connection);
            CreateTeachersTable(connection);
            CreateGradesTable(connection);
        }

        private static void CreateGradesTable(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE Grades(StudentId int FOREIGN KEY REFERENCES Students(Id),TeacherId int FOREIGN KEY REFERENCES Teachers(Id), Grade int)";
            command.ExecuteNonQuery();
        }

        private static void CreateTeachersTable(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE Teachers(Id int PRIMARY KEY IDENTITY(1,1),FirstName varchar(255),LastName varchar(255),Profession varchar(255))";
            command.ExecuteNonQuery();
        }

        private static void CreateStudentsTable(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE Students(Id int PRIMARY KEY IDENTITY(1,1), FirstName varchar(255), LastName varchar (255));";
            command.ExecuteNonQuery();
        }
    }
}

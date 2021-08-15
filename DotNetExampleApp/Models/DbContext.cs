using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetExampleApp.Models
{
    public class DbContext
    {
        public string ConnectionString { get; set; }

        public DbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Student> GetStudents()
        {
            List<Student> results = new List<Student>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from mydb.student", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new Student()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            FName = reader["fName"].ToString(),
                            LName = reader["lName"].ToString(),
                            CurrGrade = Convert.ToInt32(reader["currGrade"]),
                            Age = Convert.ToInt32(reader["age"])
                        });
                    }
                }
            }

            return results;
        }

        public void AddStudent(Student student)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                         "insert into mydb.student(fName, lName, currGrade, age) " + 
                        $"values('{student.FName}', '{student.LName}', {student.CurrGrade}, {student.Age})",
                    conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditStudent(Student student)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                          "update mydb.student set " +
                             $"fName='{student.FName}', " +
                             $"lName='{student.LName}', " +
                             $"currGrade={student.CurrGrade}, " +
                             $"age={student.Age} " +
                         $"where id={student.Id}",
                    conn); 
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteStudent(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"delete from mydb.student where id={id}", conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace EmployeePayRollAdo.Net
{
    internal class EmployeeRepository
    {
        public static string connString = @"Data Source=LAPTOP-NSJ2R3DO\SQLEXPRESS;Initial Catalog=UserDemo;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(connString);

        public bool AddEmployee(EmployeeModel emp)
        {
            SqlCommand cmd = new SqlCommand("spAddEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
            cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
            cmd.Parameters.AddWithValue("@Address", emp.Address);
            cmd.Parameters.AddWithValue("@Department", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            conn.Open();
            var result = cmd.ExecuteNonQuery();
            if (result != 0)
            {
                return true;
            }
            return false;

        }
        public void GetAllEmployee()
        {
            EmployeeModel emp = new EmployeeModel();
            using (this.conn)
            {
                string query = "select * from EmployeePayroll";
                this.conn.Open();
                SqlCommand command = new SqlCommand(query, this.conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        emp.EmpId = reader.GetInt32(0);
                        emp.EmpName = reader.GetString(1);
                        emp.PhoneNumber = reader.GetString(2);
                        emp.Address = reader.GetString(3);
                        emp.Department = reader.GetString(4);
                        emp.Salary = reader.GetInt32(5);
                        Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5} ", emp.EmpId, emp.EmpName, emp.PhoneNumber, emp.Address, emp.Department, emp.Salary);
                    }
                }
                
                else
                {
                    Console.WriteLine("No data found in the table");

                }
            }
        }
        public void UpdateEmployee()
        {
            EmployeeModel emp = new EmployeeModel();
            Console.WriteLine("Enter empid to update");
            emp.EmpId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter salary to update");
            emp.Salary = int.Parse(Console.ReadLine());
            SqlCommand command = new SqlCommand("spUpdateEmployee", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EmpId", emp.EmpId);
            command.Parameters.AddWithValue("@Salary", emp.Salary);
            conn.Open();
            command.ExecuteNonQuery();
            Console.WriteLine("Data updated successfully");
        }

        public void DeleteEmployee()
        {
            EmployeeModel emp = new EmployeeModel();
            Console.WriteLine("Enter empid to delete");
            emp.EmpId = int.Parse(Console.ReadLine());
            SqlCommand cmd = new SqlCommand("spDeleteEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
            conn.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data deleted");
        }

    }
}

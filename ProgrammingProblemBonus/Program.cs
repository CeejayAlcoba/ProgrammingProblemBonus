using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProgrammingProblemBonus
{
    public class Program
    {
        static string connectionString = @"Data Source=DESKTOP-198NBV7\SQLEXPRESS;Initial Catalog=ProgrammingProblemTwo;Integrated Security=True";
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query($@"SELECT table_name
                                              FROM information_schema.TABLES");
                PrintData(result);
            }
            Console.Read();
        }
        static void PrintData(IEnumerable<dynamic> result)
        {
            foreach (var table in result)
            {
                Console.WriteLine("Table Name : " + table.table_name);
                Console.WriteLine("Column Names: ");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var columns = connection.Query($@"SELECT column_name 
                                                   FROM information_schema.columns
                                                   Where table_name ='{table.table_name}'");
                    foreach (var column in columns)
                    {
                        Console.WriteLine(column.column_name);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
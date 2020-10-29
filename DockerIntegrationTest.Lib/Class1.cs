using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DockerIntegrationTest.Lib
{
    public class Test
    {
        public int TestId { get; set; }

        public string Name { get; set; }
    }

    public static class Class1
    {
        public static async Task CreateTables(string connString)
        {
            var query = @"-- Create a new table called 'Test' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Test', 'U') IS NOT NULL
DROP TABLE dbo.Test

-- Create the table in the specified schema
CREATE TABLE dbo.Test
(
    TestId INT NOT NULL PRIMARY KEY,
    -- primary key column
    [Name] [NVARCHAR](50) NOT NULL,
);";
            using (var connection = new SqlConnection(connString))
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async static Task SeedData(string connString)
        {
            var data = @"INSERT INTO dbo.Test(TestId, [Name]) VALUES(1, 'Test Name')";

            using (var connection = new SqlConnection(connString))
            {
                await connection.ExecuteAsync(data);
            }
        }

        public async static Task<List<Test>> GetData(string connString)
        {
            var query = "SELECT * FROM dbo.Test";

            using (var connection = new SqlConnection(connString))
            {
                var results = await connection.QueryAsync<Test>(query);
                return results.ToList();
            }
        }
    }
}

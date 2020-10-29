-- Create a new table called 'Test' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Test', 'U') IS NOT NULL
DROP TABLE dbo.Test
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Test
(
    TestId INT NOT NULL PRIMARY KEY,
    -- primary key column
    [Name] [NVARCHAR](50) NOT NULL,
);
GO
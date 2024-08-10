-- Description: https://leetcode.com/problems/second-highest-salary/

-- Example 1:
-- Input: 
-- Employee table:
-- +----+--------+
-- | id | salary |
-- +----+--------+
-- | 1  | 100    |
-- | 2  | 200    |
-- | 3  | 300    |
-- +----+--------+
-- Output: 
-- +---------------------+
-- | SecondHighestSalary |
-- +---------------------+
-- | 200                 |
-- +---------------------+

-- Example 2:
-- Input: 
-- Employee table:
-- +----+--------+
-- | id | salary |
-- +----+--------+
-- | 1  | 100    |
-- +----+--------+
-- Output: 
-- +---------------------+
-- | SecondHighestSalary |
-- +---------------------+
-- | null                |
-- +---------------------+

CREATE TABLE IF NOT EXISTS Employee (
    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
    salary NUMERIC(10, 2)
); 
INSERT INTO Employee (salary) VALUES (100); 
INSERT INTO Employee (salary) VALUES (200); 
INSERT INTO Employee (salary) VALUES (300); 

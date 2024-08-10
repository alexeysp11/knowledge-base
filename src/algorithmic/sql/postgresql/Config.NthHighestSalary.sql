
-- Example 1:
-- Input: 
-- leetcode_problems.employee table:
-- +----+--------+
-- | id | salary |
-- +----+--------+
-- | 1  | 100    |
-- | 2  | 200    |
-- | 3  | 300    |
-- | 4  | 800    |
-- | 5  | 400    |
-- | 5  | 600    |
-- | 5  | 700    |
-- +----+--------+
-- Output (4th): 
-- +---------------------+
-- | NthHighestSalary    |
-- +---------------------+
-- | 400                 |
-- +---------------------+

-- Example 2:
-- Input: 
-- leetcode_problems.employee table:
-- +----+--------+
-- | id | salary |
-- +----+--------+
-- | 1  | 100    |
-- +----+--------+
-- Output (4th): 
-- +---------------------+
-- | NthHighestSalary    |
-- +---------------------+
-- | null                |
-- +---------------------+

DROP TABLE IF EXISTS leetcode_problems.employee; 
CREATE TABLE IF NOT EXISTS leetcode_problems.employee (
    id SERIAL PRIMARY KEY NOT NULL, 
    salary NUMERIC(10, 2)
); 
INSERT INTO leetcode_problems.employee (salary) VALUES (100); 
INSERT INTO leetcode_problems.employee (salary) VALUES (200); 
INSERT INTO leetcode_problems.employee (salary) VALUES (300); 
INSERT INTO leetcode_problems.employee (salary) VALUES (800); 
INSERT INTO leetcode_problems.employee (salary) VALUES (400); 
INSERT INTO leetcode_problems.employee (salary) VALUES (600); 
INSERT INTO leetcode_problems.employee (salary) VALUES (700); 

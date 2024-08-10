
-- Example 1:
-- Input: 
-- Employee table:
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
-- Employee table:
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

DROP TABLE IF EXISTS Employee; 
CREATE TABLE IF NOT EXISTS Employee (
    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
    salary NUMERIC(10, 2)
); 
INSERT INTO Employee (salary) VALUES (100); 
INSERT INTO Employee (salary) VALUES (200); 
INSERT INTO Employee (salary) VALUES (300); 
INSERT INTO Employee (salary) VALUES (800); 
INSERT INTO Employee (salary) VALUES (400); 
INSERT INTO Employee (salary) VALUES (600); 
INSERT INTO Employee (salary) VALUES (700); 

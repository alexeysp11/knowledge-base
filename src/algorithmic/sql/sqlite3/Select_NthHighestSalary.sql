
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

DROP TABLE tmp_employee; 
CREATE TEMPORARY TABLE tmp_employee (
    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
    salary NUMERIC(10,2)
);
DELETE FROM tmp_employee; 
INSERT INTO tmp_employee (salary) SELECT DISTINCT salary FROM Employee ORDER BY salary DESC; 
SELECT t.salary FROM tmp_employee t WHERE t.id = 4; 

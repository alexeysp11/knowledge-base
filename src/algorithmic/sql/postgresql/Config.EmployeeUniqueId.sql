-- Description: https://leetcode.com/problems/replace-employee-id-with-the-unique-identifier/

-- Input: 
-- Employees table:
-- +----+----------+
-- | id | name     |
-- +----+----------+
-- | 1  | Alice    |
-- | 7  | Bob      |
-- | 11 | Meir     |
-- | 90 | Winston  |
-- | 3  | Jonathan |
-- +----+----------+
-- leetcode_problems.employee_uni table:
-- +----+-----------+
-- | id | unique_id |
-- +----+-----------+
-- | 3  | 1         |
-- | 11 | 2         |
-- | 90 | 3         |
-- +----+-----------+
-- Output: 
-- +-----------+----------+
-- | unique_id | name     |
-- +-----------+----------+
-- | null      | Alice    |
-- | null      | Bob      |
-- | 2         | Meir     |
-- | 3         | Winston  |
-- | 1         | Jonathan |
-- +-----------+----------+
-- Explanation: 
-- Alice and Bob do not have a unique ID, We will show null instead.
-- The unique ID of Meir is 2.
-- The unique ID of Winston is 3.
-- The unique ID of Jonathan is 1.

CREATE SCHEMA IF NOT EXISTS leetcode_problems; 
DROP TABLE IF EXISTS leetcode_problems.employees; 
CREATE TABLE IF NOT EXISTS leetcode_problems.employees (
    id INTEGER PRIMARY KEY, 
    name VARCHAR(50)
);
INSERT INTO leetcode_problems.employees (id, name) VALUES (1, 'Alice'); 
INSERT INTO leetcode_problems.employees (id, name) VALUES (7, 'Bob'); 
INSERT INTO leetcode_problems.employees (id, name) VALUES (11, 'Meir'); 
INSERT INTO leetcode_problems.employees (id, name) VALUES (90, 'Winston'); 
INSERT INTO leetcode_problems.employees (id, name) VALUES (3, 'Jonathan'); 

DROP TABLE IF EXISTS leetcode_problems.employee_uni; 
CREATE TABLE IF NOT EXISTS leetcode_problems.employee_uni (
    id INTEGER, 
    unique_id INTEGER
);
INSERT INTO leetcode_problems.employee_uni (id, unique_id) VALUES (3, 1); 
INSERT INTO leetcode_problems.employee_uni (id, unique_id) VALUES (11, 2); 
INSERT INTO leetcode_problems.employee_uni (id, unique_id) VALUES (90, 3); 

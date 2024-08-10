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
-- EmployeeUNI table:
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

DROP TABLE IF EXISTS Employees; 
CREATE TABLE IF NOT EXISTS Employees (
    id INTEGER PRIMARY KEY, 
    name VARCHAR(50)
);
INSERT INTO Employees (id, name) VALUES (1, 'Alice'); 
INSERT INTO Employees (id, name) VALUES (7, 'Bob'); 
INSERT INTO Employees (id, name) VALUES (11, 'Meir'); 
INSERT INTO Employees (id, name) VALUES (90, 'Winston'); 
INSERT INTO Employees (id, name) VALUES (3, 'Jonathan'); 

DROP TABLE IF EXISTS EmployeeUNI; 
CREATE TABLE IF NOT EXISTS EmployeeUNI (
    id INTEGER, 
    unique_id INTEGER
);
INSERT INTO EmployeeUNI (id, unique_id) VALUES (3, 1); 
INSERT INTO EmployeeUNI (id, unique_id) VALUES (11, 2); 
INSERT INTO EmployeeUNI (id, unique_id) VALUES (90, 3); 

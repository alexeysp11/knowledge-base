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

CREATE OR REPLACE FUNCTION leetcode_problems.f_GetEmployeeUniqueId(aEmployeeId INTEGER) 
RETURNS integer AS $$
DECLARE
  lUniqueId INTEGER;
BEGIN
    SELECT euni.unique_id 
    INTO lUniqueId 
    FROM leetcode_problems.employee_uni euni 
    WHERE euni.id = aEmployeeId;

    RETURN lUniqueId;
END;
$$ LANGUAGE plpgsql;

SELECT 
    leetcode_problems.f_GetEmployeeUniqueId(emp.id) AS unique_id, 
    emp.name
FROM leetcode_problems.employees emp; 


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
-- | 6  | 600    |
-- | 7  | 700    |
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

CREATE OR REPLACE FUNCTION leetcode_problems.f_GetNthHighestSalary(aOrderNum INTEGER)
RETURNS NUMERIC(10, 2) AS $$
DECLARE
    rec record; 
    i INTEGER; 
BEGIN 
    i := 1; 
    FOR rec IN 
        SELECT emp.salary
        FROM leetcode_problems.employee emp
        ORDER BY emp.salary DESC
    LOOP 
        IF i = aOrderNum THEN 
            RETURN rec.salary; 
        END IF; 
        i := i + 1; 
    END LOOP; 

    RETURN NULL; 
END; 
$$ LANGUAGE plpgsql;

SELECT leetcode_problems.f_GetNthHighestSalary(4) as NthHighestSalary; 

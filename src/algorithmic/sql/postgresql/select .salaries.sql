-- Question 108
-- Given two tables as below, write a query to display the comparison result (higher/lower/same) of the 
-- average salary of employees in a department to the company's average salary.

-- Table: salary
-- | id | employee_id | amount | pay_date   |
-- |----|-------------|--------|------------|
-- | 1  | 1           | 9000   | 2017-03-31 |
-- | 2  | 2           | 6000   | 2017-03-31 |
-- | 3  | 3           | 10000  | 2017-03-31 |
-- | 4  | 1           | 7000   | 2017-02-28 |
-- | 5  | 2           | 6000   | 2017-02-28 |
-- | 6  | 3           | 8000   | 2017-02-28 |

-- Table: employee_department
-- | employee_id | department_id |
-- |-------------|---------------|
-- | 1           | 1             |
-- | 2           | 2             |
-- | 3           | 2             |


select distinct 
    t.pay_month, 
    t.department_id, 
    case 
        when t.dept_avg > t.comp_avg then 'higher'
        when t.dept_avg = t.comp_avg then 'same'
        else 'lower'
    end as comparison
from 
(
    select 
        to_char(pay_date, 'yyyy-mm') as pay_month, 
        department_id, 
        avg(amount) over (partition by to_char(pay_date, 'yyyy-mm'), department_id) as dept_avg, 
        avg(amount) over (partition by to_char(pay_date, 'yyyy-mm')) as comp_avg
    from leetcode_salary s
    inner join leetcode_employee_department e on e.employee_id = s.employee_id
) t
order by t.pay_month desc, t.department_id

WITH DateIntervals AS (
    SELECT 
        Id,
        Dt AS Sd,
        LEAD(Dt) OVER (PARTITION BY Id ORDER BY Dt) AS Ed
    FROM Dates
)
SELECT Id, Sd, Ed
FROM DateIntervals
WHERE Ed IS NOT NULL OR Ed = '';

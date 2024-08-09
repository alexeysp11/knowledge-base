SELECT c.ClientName
FROM Clients c
JOIN (
    SELECT ClientId
    FROM ClientContacts
    GROUP BY ClientId
    HAVING COUNT(Id) > 2
) sub ON c.Id = sub.ClientId;

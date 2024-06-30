SELECT 
    c.ClientName, 
    COUNT(cc.Id) AS ContactCount
FROM Clients c
LEFT JOIN ClientContacts cc ON c.Id = cc.ClientId
GROUP BY c.ClientName;

-- Cached data that is used by the server application.
CREATE TABLE CachedServerValues 
(
    OrderNo INTEGER,
    Code INTEGER,
    Value TEXT 
);

-- Create tables that contain customer information.
CREATE TABLE Clients 
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ClientName NVARCHAR(200)
);
CREATE TABLE ClientContacts
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ClientId INTEGER,
    ContactType NVARCHAR(255),
    ContactValue NVARCHAR(255)
);
INSERT INTO Clients (ClientName) VALUES
    ('Client 1'),
    ('Client 2'),
    ('Client 3'),
    ('Client 4'),
    ('Client 5'),
    ('Client 6'),
    ('Client 7'),
    ('Client 8'),
    ('Client 9'),
    ('Client 10'),
    ('Client 11'),
    ('Client 12'),
    ('Client 13'),
    ('Client 14'),
    ('Client 15'),
    ('Client 16'),
    ('Client 17'),
    ('Client 18'),
    ('Client 19'),
    ('Client 20'),
    ('Client 21'),
    ('Client 22'),
    ('Client 23'),
    ('Client 24'),
    ('Client 25'),
    ('Client 26'),
    ('Client 27'),
    ('Client 28'),
    ('Client 29'),
    ('Client 30');
INSERT INTO ClientContacts (ClientId, ContactType, ContactValue) VALUES
    (1, 'Phone', '1234567890'),
    (2, 'Email', 'client2@example.com'),
    (2, 'Phone', '0987654321'),
    (3, 'Phone', '5556667777'),
    (4, 'Email', 'client4@example.com'),
    (4, 'Phone', '9998887777'),
    (6, 'Email', 'client6@example.com'),
    (7, 'Email', 'client7@example.com'),
    (7, 'Phone', '2221110000'),
    (8, 'Email', 'client8@example.com'),
    (8, 'Phone', '9990001111'),
    (9, 'Email', 'client9@example.com'),
    (9, 'Phone', '1231231234'),
    (10, 'Phone', '4564564567'),
    (11, 'Email', 'client11@example.com'),
    (12, 'Phone', '3213213210'),
    (13, 'Email', 'client13@example.com'),
    (13, 'Phone', '6546546540'),
    (14, 'Email', 'client14@example.com'),
    (14, 'Phone', '9879879870'),
    (15, 'Email', 'client15@example.com'),
    (15, 'Phone', '1471471470'),
    (16, 'Email', 'client16@example.com'),
    (17, 'Email', 'client17@example.com'),
    (17, 'Phone', '3693693690'),
    (18, 'Phone', '4804804800'),
    (19, 'Email', 'client19@example.com'),
    (19, 'Phone', '5915915910'),
    (19, 'Phone', '5915915911'),
    (20, 'Email', 'client20@example.com'),
    (21, 'Phone', '6026026020'),
    (22, 'Email', 'client22@example.com'),
    (23, 'Email', 'client3@example.com'),
    (25, 'Email', 'client25@example.com'),
    (25, 'Phone', '7778889999'),
    (26, 'Phone', '2582582580'),
    (26, 'Phone', '5554443333'),
    (27, 'Email', 'client27@example.com'),
    (28, 'Email', 'client28@example.com'),
    (29, 'Phone', '1231231234'),
    (29, 'Email', 'client29@example.com'),
    (30, 'Phone', '7897897890');


-- Create a table that contains dates.
CREATE TABLE Dates
(
    Id INTEGER,
    Dt DATE
);
INSERT INTO Dates (Id, Dt) VALUES
    (1, '01.01.2021'),
    (1, '10.01.2021'),
    (1, '30.01.2021'),
    (2, '15.01.2021'),
    (2, '30.01.2021'),
    (3, '30.01.2021');

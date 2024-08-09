CREATE TABLE EmployeeSearch_Companies (
    ID INT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    address VARCHAR(255) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    email VARCHAR(255) NOT NULL
);

CREATE TABLE EmployeeSearch_Departments (
    ID INT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    company_id INT NOT NULL,
    FOREIGN KEY (company_id) REFERENCES EmployeeSearch_Companies(ID)
);

CREATE TABLE EmployeeSearch_Positions (
    ID INT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    salary DECIMAL(10, 2) NOT NULL
);

CREATE TABLE EmployeeSearch_Specialists (
    ID INT PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    age INT NOT NULL,
    contact_info VARCHAR(255) NOT NULL
);

CREATE TABLE EmployeeSearch_Resumes (
    ID INT PRIMARY KEY,
    specialist_id INT NOT NULL,
    qualification VARCHAR(255) NOT NULL,
    experience INT NOT NULL,
    education VARCHAR(255) NOT NULL,
    FOREIGN KEY (specialist_id) REFERENCES EmployeeSearch_Specialists(ID)
);

CREATE TABLE EmployeeSearch_Vacancies (
    ID INT PRIMARY KEY,
    position_id INT NOT NULL,
    department_id INT NOT NULL,
    num_of_vacancies INT NOT NULL,
    FOREIGN KEY (position_id) REFERENCES EmployeeSearch_Positions(ID),
    FOREIGN KEY (department_id) REFERENCES EmployeeSearch_Departments(ID)
);

CREATE TABLE EmployeeSearch_InterviewStatuses (
    ID INT PRIMARY KEY,
    name VARCHAR(50)
);

CREATE TABLE EmployeeSearch_Interviews (
    ID INT PRIMARY KEY,
    vacancy_id INT NOT NULL,
    specialist_id INT NOT NULL,
    interview_date DATE NOT NULL,
    status_id INT NOT NULL,
    FOREIGN KEY (vacancy_id) REFERENCES EmployeeSearch_Vacancies(ID),
    FOREIGN KEY (specialist_id) REFERENCES EmployeeSearch_Specialists(ID),
    FOREIGN KEY (status_id) REFERENCES EmployeeSearch_InterviewStatuses(ID)
);

CREATE TABLE EmployeeSearch_OfferStatuses (
    ID INT PRIMARY KEY,
    name VARCHAR(50)
);

CREATE TABLE EmployeeSearch_Offers (
    ID INT PRIMARY KEY,
    vacancy_id INT NOT NULL,
    specialist_id INT NOT NULL,
    offer_date DATE NOT NULL,
    salary DECIMAL(10, 2) NOT NULL,
    status_id INT NOT NULL,
    FOREIGN KEY (vacancy_id) REFERENCES EmployeeSearch_Vacancies(ID),
    FOREIGN KEY (specialist_id) REFERENCES EmployeeSearch_Specialists(ID),
    FOREIGN KEY (status_id) REFERENCES EmployeeSearch_OfferStatuses(ID)
);

CREATE TABLE EmployeeSearch_Contracts (
    ID INT PRIMARY KEY,
    offer_id INT NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    FOREIGN KEY (offer_id) REFERENCES EmployeeSearch_Offers(ID)
);

CREATE TABLE EmployeeSearch_Feedbacks (
    ID INT PRIMARY KEY,
    contract_id INT NOT NULL,
    rating INT NOT NULL,
    comment VARCHAR(255) NOT NULL,
    FOREIGN KEY (contract_id) REFERENCES EmployeeSearch_Contracts(ID)
);

CREATE TABLE EmployeeSearch_DbgLogs
(
    Id SERIAL PRIMARY KEY,
	TransactionName VARCHAR(150),
	TransactionDetails TEXT,
	TransactionStatus VARCHAR(50),
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ChangeDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-------

INSERT INTO EmployeeSearch_Companies (ID, name, address, phone, email) VALUES
(1, 'Google', '1600 Amphitheatre Parkway, Mountain View, CA 94043', '+1 650-253-0000', 'contact@google.com'),
(2, 'Microsoft', 'One Microsoft Way, Redmond, WA 98052', '+1 425-882-8080', 'contact@microsoft.com'),
(3, 'Apple', '1 Apple Park Way, Cupertino, CA 95014', '+1 408-996-1010', 'contact@apple.com'),
(4, 'Sony', 'Kōnan, Minato, Tokyo, Japan', '1-800-634-8606', 'playstationcares@sony.com'),
(5, 'Hitachi', 'Marunouchi, Chiyoda-ku, Tokyo, Japan', '+49 (0)211 5283-201', 'Maik.Naphausen@hitachi-eu.com'),
(6, 'Huawei', 'Shenzhen, China', '+852-800-931-122', 'corporate.comms@huawei.com'),
(7, 'Panasonic', '	Kadoma, Osaka, Japan', '+1-905-624-5010', 'pids@ca.panasonic.com'),
(8, 'Lenovo', 'Morrisville, North Carolina, United States', '1-855-253-6686', 'Lencare@lenovo.com'),
(9, 'Alphabet Inc.', 'Googleplex, Mountain View, California, U.S.', '0370 0120 396', 'admin.support@alphabet.co.uk'),
(10, 'Dell Technologies', 'Round Rock, Texas, U.S.', '+1 512 728 7800', 'investor_relations@dell.com');

INSERT INTO EmployeeSearch_Departments (ID, name, company_id) VALUES
(1, 'Engineering', 1),
(2, 'Marketing', 1),
(3, 'Sales', 2),
(4, 'Finance', 3);

INSERT INTO EmployeeSearch_Positions (ID, name, salary) VALUES
(1, 'Software Engineer', 100000),
(2, 'Marketing Manager', 80000),
(3, 'Sales Representative', 60000),
(4, 'Financial Analyst', 75000);

INSERT INTO EmployeeSearch_Specialists (ID, first_name, last_name, age, contact_info) VALUES
(1, 'John', 'Doe', 30, 'john.doe@gmail.com'),
(2, 'Jane', 'Smith', 25, 'jane.smith@yahoo.com'),
(3, 'Bob', 'Johnson', 40, 'bob.johnson@hotmail.com'),
(4, 'Michael', 'Brown', 28, 'michael.brown@gmail.com'),
(5, 'Emily', 'Davis', 32, 'emily.davis@yahoo.com'),
(6, 'William', 'Garcia', 27, 'william.garcia@hotmail.com'),
(7, 'Sophia', 'Martinez', 31, 'sophia.martinez@gmail.com'),
(8, 'Robert', 'Anderson', 29, 'robert.anderson@yahoo.com'),
(9, 'Avery', 'Thomas', 26, 'avery.thomas@hotmail.com'),
(10, 'Madison', 'Jackson', 30, 'madison.jackson@gmail.com'),
(11, 'Ethan', 'White', 28, 'ethan.white@yahoo.com'),
(12, 'Olivia', 'Harris', 32, 'olivia.harris@hotmail.com'),
(13, 'Isabella', 'Martin', 27, 'isabella.martin@gmail.com'),
(14, 'Mason', 'Thompson', 31, 'mason.thompson@yahoo.com'),
(15, 'Jacob', 'Moore', 29, 'jacob.moore@hotmail.com'),
(16, 'Emma', 'Allen', 26, 'emma.allen@gmail.com'),
(17, 'Noah', 'King', 30, 'noah.king@yahoo.com'),
(18, 'Ava', 'Wright', 28, 'ava.wright@hotmail.com'),
(19, 'Liam', 'Scott', 32, 'liam.scott@gmail.com'),
(20, 'Mia', 'Walker', 27, 'mia.walker@yahoo.com');

INSERT INTO EmployeeSearch_Resumes (ID, specialist_id, qualification, experience, education) VALUES
(1, 1, 'Bachelor of Science in Computer Science', 5, 'Stanford University'),
(2, 2, 'Master of Business Administration', 2, 'Harvard University'),
(3, 3, 'Bachelor of Science in Accounting', 10, 'University of California, Berkeley'),
(4, 4, 'Bachelor of Science in Electrical Engineering', 3, 'Massachusetts Institute of Technology'),
(5, 5, 'Master of Business Administration', 5, 'University of Pennsylvania'),
(6, 6, 'Bachelor of Science in Computer Science', 2, 'California Institute of Technology'),
(7, 7, 'Master of Science in Marketing', 4, 'New York University'),
(8, 8, 'Bachelor of Science in Finance', 6, 'University of Chicago'),
(9, 9, 'Master of Science in Computer Engineering', 1, 'Georgia Institute of Technology'),
(10, 10, 'Bachelor of Science in Marketing', 3, 'University of Michigan'),
(11, 11, 'Master of Science in Finance', 5, 'Columbia University'),
(12, 12, 'Bachelor of Science in Computer Engineering', 2, 'University of Illinois at Urbana-Champaign'),
(13, 13, 'Master of Science in Marketing', 4, 'Duke University'),
(14, 14, 'Bachelor of Science in Finance', 6, 'University of California, Los Angeles'),
(15, 15, 'Master of Science in Computer Science', 1, 'Carnegie Mellon University'),
(16, 16, 'Bachelor of Science in Marketing', 3, 'University of Texas at Austin'),
(17, 17, 'Master of Science in Finance', 5, 'University of Virginia'),
(18, 18, 'Bachelor of Science in Computer Science', 2, 'University of California, San Diego'),
(19, 19, 'Master of Science in Marketing', 4, 'University of North Carolina at Chapel Hill'),
(20, 20, 'Bachelor of Science in Finance', 6, 'University of Southern California'),
(21, 4, 'Master of Science in Electrical Engineering', 5, 'Stanford University'),
(22, 5, 'Bachelor of Science in Business Administration', 2, 'University of Notre Dame'),
(23, 6, 'Master of Science in Computer Science', 3, 'Massachusetts Institute of Technology'),
(24, 7, 'Bachelor of Science in Marketing', 4, 'University of California, Berkeley'),
(25, 8, 'Master of Science in Finance', 6, 'New York University'),
(26, 9, 'Bachelor of Science in Computer Engineering', 1, 'University of Michigan'),
(27, 10, 'Master of Science in Marketing', 3, 'Duke University'),
(28, 11, 'Bachelor of Science in Finance', 5, 'University of Virginia'),
(29, 12, 'Master of Science in Computer Engineering', 2, 'Georgia Institute of Technology'),
(30, 13, 'Bachelor of Science in Marketing', 4, 'University of Pennsylvania'),
(31, 14, 'Master of Science in Finance', 6, 'Columbia University'),
(32, 15, 'Bachelor of Science in Computer Science', 1, 'California Institute of Technology'),
(33, 16, 'Master of Science in Marketing', 3, 'University of North Carolina at Chapel Hill'),
(34, 17, 'Bachelor of Science in Finance', 5, 'University of Chicago'),
(35, 18, 'Master of Science in Computer Science', 2, 'Carnegie Mellon University'),
(36, 19, 'Bachelor of Science in Marketing', 4, 'New York University'),
(37, 20, 'Master of Science in Finance', 6, 'University of California, Los Angeles');

INSERT INTO EmployeeSearch_Vacancies (ID, position_id, department_id, num_of_vacancies) VALUES
(1, 1, 1, 2),
(2, 2, 1, 1),
(3, 3, 2, 3),
(4, 4, 2, 2),
(5, 1, 3, 1);

INSERT INTO EmployeeSearch_InterviewStatuses (ID, name) VALUES
(1, 'Scheduled'),
(2, 'Completed'),
(3, 'Cancelled');

INSERT INTO EmployeeSearch_Interviews (ID, vacancy_id, specialist_id, interview_date, status_id) VALUES
(1, 2, 3, '2021-07-12', 1),
(2, 5, 4, '2021-07-13', 2),
(3, 1, 6, '2021-07-14', 3),
(4, 3, 8, '2021-07-15', 1),
(5, 4, 7, '2021-07-16', 2),
(6, 2, 9, '2021-07-17', 3),
(7, 5, 11, '2021-07-18', 1),
(8, 1, 10, '2021-07-19', 2),
(9, 3, 12, '2021-07-20', 3),
(10, 4, 14, '2021-07-21', 1),
(11, 2, 13, '2021-07-22', 2),
(12, 5, 15, '2021-07-23', 3),
(13, 1, 17, '2021-07-24', 1),
(14, 3, 16, '2021-07-25', 2),
(15, 4, 18, '2021-07-26', 3),
(16, 2, 20, '2021-07-27', 1),
(17, 5, 19, '2021-07-28', 2),
(18, 1, 1, '2021-07-29', 3),
(19, 3, 3, '2021-07-30', 1),
(20, 4, 2, '2021-07-31', 2),
(21, 1, 4, '2022-02-01', 1),
(22, 2, 5, '2022-02-02', 1),
(23, 3, 6, '2022-02-03', 1),
(24, 4, 7, '2022-02-04', 1),
(25, 5, 8, '2022-02-05', 1),
(26, 1, 4, '2022-02-01', 1),
(27, 2, 5, '2022-02-02', 1),
(28, 3, 6, '2022-02-03', 1),
(29, 4, 7, '2022-02-04', 1),
(30, 5, 8, '2022-02-05', 1);

INSERT INTO EmployeeSearch_OfferStatuses (ID, name) VALUES
(1, 'Pending'),
(2, 'Accepted'),
(3, 'Declined');

INSERT INTO EmployeeSearch_Offers (ID, vacancy_id, specialist_id, offer_date, salary, status_id) VALUES
(1, 1, 4, '2022-02-06', 50000.00, 1),
(2, 3, 6, '2022-02-07', 60000.00, 1),
(3, 4, 7, '2022-02-08', 55000.00, 1),
(4, 5, 9, '2022-02-09', 65000.00, 1),
(5, 2, 5, '2022-02-10', 70000.00, 1);

INSERT INTO EmployeeSearch_Contracts (ID, offer_id, start_date, end_date) VALUES
(1, 1, '2022-03-01', '2023-03-01'),
(2, 3, '2022-04-01', '2023-04-01'),
(3, 4, '2022-05-01', '2023-05-01'),
(4, 5, '2022-06-01', '2023-06-01'),
(5, 2, '2022-07-01', '2023-07-01');

INSERT INTO EmployeeSearch_Feedbacks (ID, contract_id, rating, comment) VALUES
(1, 1, 4, 'The employee is doing great work.'),
(2, 3, 3, 'The employee needs to improve their communication skills.'),
(3, 4, 5, 'The employee is an excellent addition to the team.'),
(4, 5, 4, 'The employee has exceeded our expectations.'),
(5, 2, 2, 'The employee did not meet our expectations.');

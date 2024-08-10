drop table if exists DapperPerson;
drop table if exists EfCorePerson;
create table if not exists DapperPerson
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name varchar(100),
    description varchar(255)
);
create table if not exists EfCorePerson
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name varchar(100),
    description varchar(255)
);

create table if not exists testisolation
(
    id serial not null,
    value text
);

insert into testisolation (value) 
values 
    ('value1'),
    ('value2'),
    ('value3');
create table table_generatingseries
(
	id serial not null,
	name varchar(50) not null,
	number integer
);
insert into table_generatingseries (name, number)
select md5(random()::text) as name, generate_series(0,100000) as number;

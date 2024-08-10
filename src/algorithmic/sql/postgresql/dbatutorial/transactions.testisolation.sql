-- The following transactions should be run simultaniously and separately, in two different terminals (e.g. psql).
-- The transaction should be executed manually, line by line.
-- Goal: to see what rows would look like in other transactions using different isolation levels.

-------------------------------------------

-- ISOLATION LEVEL: read committed.
-- Transaction A1.
begin isolation level read committed;
select t.id, t.value, t.xmin, t.xmax from testisolation t;
-- Stop here and run transaction A2 entirely.
select t.id, t.value, t.xmin, t.xmax from testisolation t;
commit;
-- Transaction A2.
begin;
update testisolation set value = 'value1 (updated 1)' where id = 1;
insert into testisolation (value) values ('value4');
commit;

-- Transaction B1.
begin;
create table if not exists testisolation_rollback (id serial not null, value text);
-- Stop here and run transaction B2 entirely.
rollback;
-- Transaction B2.
begin isolation level read committed;
select t.id, t.value, t.xmin, t.xmax from testisolation_rollback t;
commit;

-------------------------------------------

-- ISOLATION LEVEL: repeatable read.
-- Transaction A1.
begin isolation level repeatable read;
select t.id, t.value, t.xmin, t.xmax from testisolation t;
-- Stop here and run transaction A2 entirely.
select t.id, t.value, t.xmin, t.xmax from testisolation t;
commit;
-- Transaction A2.
begin;
update testisolation set value = 'value1 (updated 2)' where id = 1;
insert into testisolation (value) values ('value5');
commit;

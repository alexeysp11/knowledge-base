-- SlotsInCalendar

-- Example 1: 
-- calendar1: [[8:15-9:30], [12:30-14:00], [18:00-20:00]], dailyBound1: [8:00-20:00]
-- calendar2: [[9:00-11:30], [13:00-14:30], [15:00-17:45]], dailyBound2: [8:00-18:00]
-- duration: 0:30
-- output: [[11:30-12:30], [14:30-15:00]]

DROP TABLE IF EXISTS person; 
CREATE TABLE IF NOT EXISTS person (
    person_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
    name VARCHAR(20)
); 
INSERT INTO person (name) VALUES ('Person 1'); 
INSERT INTO person (name) VALUES ('Person 2'); 

DROP TABLE IF EXISTS daily_bound; 
CREATE TABLE IF NOT EXISTS daily_bound (
    daily_bound_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
    person_id INTEGER,
    begin_datetime DATETIME,
    end_datetime DATETIME
);
INSERT INTO daily_bound (person_id, begin_datetime, end_datetime) VALUES (1, strftime('%Y-%m-%d 08:00'), strftime('%Y-%m-%d 20:00')); 
INSERT INTO daily_bound (person_id, begin_datetime, end_datetime) VALUES (2, strftime('%Y-%m-%d 08:00'), strftime('%Y-%m-%d 18:00')); 

DROP TABLE IF EXISTS activity; 
CREATE TABLE IF NOT EXISTS activity (
    activity_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    name VARCHAR(20),
    person_id INTEGER,
    begin_datetime DATETIME,
    end_datetime DATETIME
);
INSERT INTO activity (name, person_id, begin_datetime, end_datetime) VALUES ('Activity 1', 1, strftime('%Y-%m-%d 08:15'), strftime('%Y-%m-%d 09:30')); 
INSERT INTO activity (name, person_id, begin_datetime, end_datetime) VALUES ('Activity 2', 1, strftime('%Y-%m-%d 12:30'), strftime('%Y-%m-%d 14:00')); 
INSERT INTO activity (name, person_id, begin_datetime, end_datetime) VALUES ('Activity 3', 1, strftime('%Y-%m-%d 18:00'), strftime('%Y-%m-%d 20:00')); 
INSERT INTO activity (name, person_id, begin_datetime, end_datetime) VALUES ('Activity 5', 2, strftime('%Y-%m-%d 09:00'), strftime('%Y-%m-%d 11:30')); 
INSERT INTO activity (name, person_id, begin_datetime, end_datetime) VALUES ('Activity 7', 2, strftime('%Y-%m-%d 13:00'), strftime('%Y-%m-%d 14:30')); 
INSERT INTO activity (name, person_id, begin_datetime, end_datetime) VALUES ('Activity 6', 2, strftime('%Y-%m-%d 15:00'), strftime('%Y-%m-%d 17:45')); 

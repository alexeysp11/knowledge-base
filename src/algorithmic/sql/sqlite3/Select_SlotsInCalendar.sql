-- SlotsInCalendar

-- Example 1: 
-- calendar1: [[8:15-9:30], [12:30-14:00], [18:00-20:00]], dailyBound1: [8:00-20:00]
-- calendar2: [[9:00-11:30], [13:00-14:30], [15:00-17:45]], dailyBound2: [8:00-18:00]
-- duration: 0:30
-- output: [[11:30-12:30], [14:30-15:00]]

-- get person and their daily bounds for today
select 
    p.person_id as person_id, 
    --p.name as person_name, 
    d.begin_datetime as daily_begin, 
    d.end_datetime as daily_end 
from person p 
left join daily_bound d on d.person_id = p.person_id
where d.begin_datetime >= strftime('%Y-%m-%d 00:00')
    and d.end_datetime <= strftime('%Y-%m-%d 23:59');

-- get a list of busy slots for each person in today's daily bounds 
select 
    p.person_id as person_id, 
    --p.name as person_name, 
    a.begin_datetime as activity_begin, 
    a.end_datetime as activity_end, 
    d.begin_datetime as daily_begin, 
    d.end_datetime as daily_end 
from person p 
left join activity a on a.person_id = p.person_id
left join daily_bound d on d.person_id = p.person_id
where d.begin_datetime >= strftime('%Y-%m-%d 00:00')
    and d.end_datetime <= strftime('%Y-%m-%d 23:59')
    and a.begin_datetime >= d.begin_datetime 
    and a.end_datetime <= d.end_datetime;

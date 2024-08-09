using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Studying.Leetcode.Orm 
{
    /// <summary>
    /// Write an algorithm that takes two people's calendars and returns free slots of time,
    /// during which these two people can have a meeting. 
    /// These two people also have daily bounds when it's okay for them to have a meeting. 
    /// You are given a desirable duration of a meeting. 
    /// </summary>
    public class SlotsInCalendar : Studying.Leetcode.ILeetcodeProblem 
    {
        private class SlotsInCalendarContext : DbContext
        {
            public DbSet<Person> Persons { get; set; }
            public DbSet<DailyBound> DailyBounds { get; set; }
            public DbSet<Activity> Activities { get; set; }

            public string DbPath { get; }

            public SlotsInCalendarContext()
            {
                var folder = System.IO.Path.Join(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\data\sqlite3");
                DbPath = System.IO.Path.Join(folder, "SlotsInCalendar.db");
            }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite($"Data Source={DbPath}");
        }

        [Table("person")]
        private class Person
        {
            [Column("person_id")]
            public int PersonId { get; set; }
            [Column("name")]
            public string Name { get; set; }

            public List<Activity> Activities { get; } = new List<Activity>(); 
            public List<DailyBound> DailyBounds { get; } = new List<DailyBound>(); 
        }

        [Table("daily_bound")]
        private class DailyBound 
        {
            [Column("daily_bound_id")]
            public int DailyBoundId { get; set; }
            [Column("begin_datetime")]
            public System.DateTime BeginDateTime { get; set; }
            [Column("end_datetime")]
            public System.DateTime EndDateTime { get; set; }

            [Column("person_id")]
            public int PersonId { get; set; }
            public Person Person { get; set; }
        }

        [Table("activity")]
        private class Activity
        {
            [Column("activity_id")]
            public int ActivityId { get; set; }
            [Column("name")]
            public string Name { get; set; }
            [Column("begin_datetime")]
            public System.DateTime BeginDateTime { get; set; }
            [Column("end_datetime")]
            public System.DateTime EndDateTime { get; set; }

            [Column("person_id")]
            public int PersonId { get; set; }
            public Person Person { get; set; }
        }
        
        /// <summary>
        /// Represents any slot in a person's calendar (e.g. meeting, lunch, daily bound, free slot etc)
        /// </summary>
        private class TimeSlot 
        {
            public System.DateTime Begin { get; } 
            public System.DateTime End { get; }

            public TimeSlot(System.DateTime begin, System.DateTime end)
            {
                // Constraint: 
                // 1. The beginning of a time slot should not be bigger than its end. 

                if (begin > end) 
                    throw new System.Exception("Incorrect borders of a time slot: begin should not be bigger than end"); 

                Begin = begin; 
                End = end; 
            }

            public System.TimeSpan GetDuration()
            {
                return End.Subtract(Begin); 
            }
            public bool HasIntersectionWith(TimeSlot slot)
            {
                if (slot == null)
                {
                    return false;
                }

                if (this.End < slot.Begin || this.Begin > slot.End)
                {
                    return false;
                }

                return true;
            }
            public override string ToString()
            {
                return "[" + Begin.ToString() + "-" + End.ToString() + "]"; 
            }
        }

        public void Execute()
        {
            System.Console.WriteLine("Slots of time in a calendar of two people (ORM approach)\n".ToUpper()); 
            
            System.TimeSpan duration = new System.TimeSpan(0, 30, 0);

            using var db = new SlotsInCalendarContext();
            Dictionary<int, List<TimeSlot>> freeSlots = GetFreeSlots(db);

            // Get two lists of free slots using freeSlots. 
            // Note: in this example, we assume that we already know person.person_id, but in general in order to assign 
            // a lists of free slots, it's better to use that dictionary, which was initialized above.
            List<TimeSlot> freeSlots1 = freeSlots[1]; 
            List<TimeSlot> freeSlots2 = freeSlots[2]; 

            // Get all intersections between free time slots 
            List<TimeSlot> tmp = new List<TimeSlot>(); 
            foreach (TimeSlot slot1 in freeSlots1)
            {
                foreach (TimeSlot slot2 in freeSlots2)
                {
                    if (slot1.HasIntersectionWith(slot2))
                    {
                        System.DateTime begin = slot1.Begin > slot2.Begin ? slot1.Begin : slot2.Begin; 
                        System.DateTime end = slot1.End < slot2.End ? slot1.End : slot2.End; 
                        tmp.Add(new TimeSlot(begin, end)); 
                    }
                }
            }

            // Filter by duration 
            List<TimeSlot> result = new List<TimeSlot>(); 
            foreach (var item in tmp) if (item.GetDuration() >= duration) result.Add(item); 
            
            foreach (var item in result) System.Console.WriteLine($"item: {item.ToString()}"); 
        }

        private Dictionary<int, List<TimeSlot>> GetFreeSlots(SlotsInCalendarContext db)
        {
            System.Console.WriteLine($"Database path: {db.DbPath}.");
            System.Console.WriteLine("Querying...");

            Dictionary<int, List<TimeSlot>> freeSlots = new Dictionary<int, List<TimeSlot>>();
            var persons = db.Persons.Take(2).OrderBy(p => p.PersonId); 
            foreach (var person in persons) 
            {
                var activities = (from activity in db.Activities.OrderBy(a => a.ActivityId)
                                where activity.PersonId == person.PersonId
                                select activity).ToList();
                var bound = (from dailyBound in db.DailyBounds.OrderBy(x => x.DailyBoundId)
                                where dailyBound.PersonId == person.PersonId
                                select dailyBound).First();
                List<TimeSlot> slots = new List<TimeSlot>(); 
                if (activities.Count == 0) 
                {
                    slots.Add(new TimeSlot(bound.EndDateTime, bound.BeginDateTime)); 
                }
                else 
                {
                    // Get slots before activities 
                    if (bound.BeginDateTime < activities.First().BeginDateTime) 
                        slots.Add(new TimeSlot(bound.BeginDateTime, activities.First().BeginDateTime));

                    // Get slots between activities 
                    for (int i = 0; i < activities.Count - 1; i++)
                        slots.Add(new TimeSlot(activities[i].EndDateTime, activities[i+1].BeginDateTime)); 

                    // Get slots after activities
                    if (bound.EndDateTime > activities.Last().EndDateTime) 
                        slots.Add(new TimeSlot(activities.Last().EndDateTime, bound.EndDateTime)); 
                }
                try { freeSlots.Add(person.PersonId, slots); } catch (System.ArgumentException ex) {}
            }
            return freeSlots; 
        }
    }
}
using System.Collections.Generic; 

namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// Write an algorithm that takes two people's calendars and returns free slots of time,
    /// during which these two people can have a meeting. 
    /// These two people also have daily bounds when it's okay for them to have a meeting. 
    /// You are given a desirable duration of a meeting. 
    /// </summary>
    public class SlotsInCalendar : Studying.Leetcode.ILeetcodeProblem 
    {
        private class TimeOnly
        {
            #region Public properties
            public int Hour { get; } 
            public int Minute { get; } 
            public int Second { get; } 
            #endregion  // Public properties

            #region Constructors
            public TimeOnly(int hour, int minute, int second=0)
            {
                if (hour < 0 || hour > 24) 
                    throw new System.Exception("Error while initialing TimeOnly: hour should be in the range between 0 and 24"); 
                if (minute < 0 || minute > 60) 
                    throw new System.Exception("Error while initialing TimeOnly: minute should be in the range between 0 and 60"); 
                if (second < 0 || second > 60) 
                    throw new System.Exception("Error while initialing TimeOnly: second should be in the range between 0 and 60"); 

                Hour = hour; 
                Minute = minute; 
                Second = second; 
            }
            #endregion  // Constructors

            #region Comparasion operators
            public static bool operator >(TimeOnly a, TimeOnly b) => a.IsBiggerThan(b);
            public static bool operator <(TimeOnly a, TimeOnly b) => a.IsLessThan(b);
            public static bool operator ==(TimeOnly a, TimeOnly b) => a.Equals(b);
            public static bool operator !=(TimeOnly a, TimeOnly b) => !a.Equals(b);
            public static bool operator >=(TimeOnly a, TimeOnly b) => a.IsBiggerOrEqual(b);
            public static bool operator <=(TimeOnly a, TimeOnly b) => a.IsLessOrEqual(b);
            #endregion  // Comparasion operators
            
            #region Comparasion methods 
            public bool IsBiggerThan(TimeOnly timeOnly)
            {
                if (this.Hour > timeOnly.Hour) return true; 
                if (this.Hour == timeOnly.Hour && this.Minute > timeOnly.Minute) return true; 
                if (this.Hour == timeOnly.Hour && this.Minute == timeOnly.Minute && this.Second > timeOnly.Second) return true; 
                return false; 
            }
            public bool IsLessThan(TimeOnly timeOnly)
            {
                return timeOnly.IsBiggerThan(this); 
            }
            public override bool Equals(object obj)
            {
                if (obj == null || ! this.GetType().Equals(obj.GetType())) return false; 
                TimeOnly timeOnly = (TimeOnly)obj; 
                return this.Hour == timeOnly.Hour && this.Minute == timeOnly.Minute && this.Second == timeOnly.Second;
            }
            public bool IsBiggerOrEqual(TimeOnly timeOnly)
            {
                return this.IsBiggerThan(timeOnly) || this.Equals(timeOnly); 
            }
            public bool IsLessOrEqual(TimeOnly timeOnly)
            {
                return this.IsLessThan(timeOnly) || this.Equals(timeOnly); 
            }
            #endregion  // Comparasion methods 

            #region Conversion methods
            public System.TimeSpan ToTimeSpan()
            {
                return new System.TimeSpan(Hour, Minute, Second); 
            }
            public override string ToString()
            {
                return Hour + ":" + Minute + ":" + Second; 
            }
            #endregion  // Conversion methods
        }
        
        /// <summary>
        /// Represents any slot in a person's calendar (e.g. meeting, lunch, daily bound, free slot etc)
        /// </summary>
        private class TimeSlot 
        {
            public TimeOnly Begin { get; } 
            public TimeOnly End { get; }

            public TimeSlot(TimeOnly begin, TimeOnly end)
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
                return End.ToTimeSpan() - Begin.ToTimeSpan(); 
            }
            public bool HasIntersectionWith(TimeSlot slot)
            {
                // No intersection: 
                // |-------|
                //            |-----| 
                if (this.End < slot.Begin || this.Begin > slot.End) return false; 

                // Partial overlap: 
                // |-----------|
                //      |---------| 
                // Containment: 
                // |-----------|
                //    |-----|
                // Full overlap: 
                // |-----------|
                // |-----------|
                if ((this.Begin <= slot.Begin && this.End >= slot.Begin) || (this.Begin >= slot.Begin && slot.End >= this.Begin)) return true; 

                return false; 
            }
            public override string ToString()
            {
                return "[" + Begin.ToString() + "-" + End.ToString() + "]"; 
            }
        }

        private class Calendar 
        {
            public List<TimeSlot> Activities { get; }
            public TimeSlot DailyBound { get; }
            private List<TimeSlot> FreeTimeSlots { get; set; } = null; 

            private string ActivitiesString { get; set; }
            private string FreeTimeSlotsString { get; set; }

            public Calendar(List<TimeSlot> activities, TimeSlot dailyBound)
            {
                // Constraints: 
                // 1. The list of activities is represented in ascending order. 
                // 2. The first element of the list of activities should not be less than the beginning of daily bound, 
                // and the last element of the list should not be bigger than the end of daily bound. 

                if (activities.Count > 0) 
                {
                    TimeOnly prevBegin = activities[0].Begin; 
                    TimeOnly prevEnd = activities[0].End; 
                    foreach (TimeSlot activity in activities) 
                    {
                        if (prevBegin > activity.Begin || prevEnd > activity.End) 
                            throw new System.Exception("Violated constraint: incorrect order in a list of activities"); 
                        
                        prevBegin = activity.Begin; 
                        prevEnd = activity.End; 
                    }
                    if (activities[0].Begin < dailyBound.Begin && activities[activities.Count - 1].End > dailyBound.End) 
                        throw new System.Exception("Violated constraint: activities are out of daily bound"); 
                }

                Activities = activities; 
                DailyBound = dailyBound; 
            }

            public List<TimeSlot> GetFreeTimeSlots()
            {
                if (FreeTimeSlots == null) 
                {
                    List<TimeSlot> slots = new List<TimeSlot>(); 
                    if (Activities.Count == 0) 
                    {
                        slots.Add(new TimeSlot(DailyBound.End, DailyBound.Begin)); 
                    }
                    else 
                    {
                        // Get slots before activities 
                        if (DailyBound.Begin < Activities[0].Begin) 
                            slots.Add(new TimeSlot(DailyBound.Begin, Activities[0].Begin));

                        // Get slots between activities 
                        for (int i = 0; i < Activities.Count - 1; i++)
                            slots.Add(new TimeSlot(Activities[i].End, Activities[i+1].Begin)); 

                        // Get slots after activities
                        if (DailyBound.End > Activities[Activities.Count - 1].End) 
                            slots.Add(new TimeSlot(Activities[Activities.Count - 1].End, DailyBound.End)); 
                    }
                    FreeTimeSlots = slots; 
                }
                return FreeTimeSlots; 
            }

            #region Conversion methods 
            public string ToActivitiesString()
            {
                if (string.IsNullOrEmpty(ActivitiesString))
                {
                    string result = "["; 
                    foreach (TimeSlot activity in Activities) result += activity.ToString() + ","; 
                    result += "]"; 
                    ActivitiesString = result.Replace(",]", "]"); 
                }
                return ActivitiesString; 
            }
            public string ToDailyBoundString()
            {
                return DailyBound.ToString(); 
            }
            public string ToFreeTimeSlotsString()
            {
                if (string.IsNullOrEmpty(FreeTimeSlotsString)) 
                {
                    string result = "["; 
                    List<TimeSlot> slots = GetFreeTimeSlots(); 
                    foreach (TimeSlot slot in slots) result += slot.ToString() + ","; 
                    result += "]"; 
                    FreeTimeSlotsString = result.Replace(",]", "]"); 
                }
                return FreeTimeSlotsString; 
            }
            public override string ToString()
            {
                return "activities: " + ToActivitiesString() + ", daily bound:" + ToDailyBoundString(); 
            }
            #endregion  // Conversion methods 
        }

        public void Execute()
        {
            System.Console.WriteLine("Slots of time in a calendar of two people\n".ToUpper()); 

            // Example 1: 
            // calendar1: [[8:15-9:30], [12:30-14:00], [18:00-20:00]], dailyBound1: [8:00-20:00]
            // calendar2: [[9:00-11:30], [13:00-14:30], [15:00-17:45]], dailyBound2: [8:00-18:00]
            // duration: 0:30
            // output: [[11:30-12:30], [14:30-15:00]]
            Calendar calendar11 = new Calendar(
                new List<TimeSlot>() { 
                    new TimeSlot(new TimeOnly(8, 15), new TimeOnly(9, 30)),  
                    new TimeSlot(new TimeOnly(12, 30), new TimeOnly(14, 0)),  
                    new TimeSlot(new TimeOnly(18, 0), new TimeOnly(20, 0)) 
                }, 
                new TimeSlot(new TimeOnly(8, 0), new TimeOnly(20, 0))
            );
            Calendar calendar12 = new Calendar(
                new List<TimeSlot>() { 
                    new TimeSlot(new TimeOnly(9, 0), new TimeOnly(11, 30)),  
                    new TimeSlot(new TimeOnly(13, 0), new TimeOnly(14, 30)),  
                    new TimeSlot(new TimeOnly(15, 0), new TimeOnly(17, 45)) 
                }, 
                new TimeSlot(new TimeOnly(8, 0), new TimeOnly(18, 0))
            );
            TimeOnly duration1 = new TimeOnly(0, 30); 
            List<TimeSlot> output1 = Solve(calendar11, calendar12, duration1);
            System.Console.WriteLine("calendar1: " + calendar11.ToString()); 
            System.Console.WriteLine("calendar2: " + calendar12.ToString()); 
            System.Console.WriteLine("duration1: " + duration1.ToString()); 
            System.Console.WriteLine("output: " + GetFreeSlotsString(output1)); 
        }

        private List<TimeSlot> Solve(Calendar calendar1, Calendar calendar2, TimeOnly duration)
        {
            List<TimeSlot> tmp = new List<TimeSlot>(); 
            List<TimeSlot> result = new List<TimeSlot>(); 
            List<TimeSlot> freeSlots1 = calendar1.GetFreeTimeSlots(); 
            List<TimeSlot> freeSlots2 = calendar2.GetFreeTimeSlots(); 

            // Get all intersections 
            foreach (TimeSlot slot1 in freeSlots1)
            {
                foreach (TimeSlot slot2 in freeSlots2)
                {
                    if (slot1.HasIntersectionWith(slot2))
                    {
                        TimeOnly begin = slot1.Begin > slot2.Begin ? slot1.Begin : slot2.Begin; 
                        TimeOnly end = slot1.End < slot2.End ? slot1.End : slot2.End; 
                        tmp.Add(new TimeSlot(begin, end)); 
                    }
                }
            }

            // Filter by duration 
            foreach (var item in tmp) if (item.GetDuration() >= duration.ToTimeSpan()) result.Add(item); 

            return result; 
        }

        private string GetFreeSlotsString(List<TimeSlot> list)
        {
            string result = "["; 
            foreach (var item in list)
            {
                result += item.ToString() + ","; 
            }
            result += "]"; 
            return result.Replace(",]", "]"); 
        }
    }
}

using System;

namespace petder.Providers
{ 
    public class DateTimeProvider : IDateTimeProvider
    {
        protected DateTime dateTimeNow;

        public DateTimeProvider() 
        {
            dateTimeNow = DateTime.UtcNow;
        }
        public DateTime DateTimeNow()
        {
            return dateTimeNow;
        }

        public TimeSpan TimeSpanNow()
        {
            return dateTimeNow.TimeOfDay;
        }

        public DateTime SpecifyUtcKind(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }
    }
}
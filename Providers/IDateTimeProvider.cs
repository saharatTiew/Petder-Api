using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace petder.Providers
{
    public interface IDateTimeProvider
    {
        DateTime DateTimeNow();
        TimeSpan TimeSpanNow();
        DateTime SpecifyUtcKind(DateTime dateTime);
    }
}
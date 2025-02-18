namespace EverythingNet.Interfaces;

using System;

public enum Dates
{
    Today,
    Yesterday,
    ThisWeek,
    ThisMonth,
    ThisYear,
    ThisSunday,
    ThisMonday,
    ThisTuesday,
    ThisWednesday,
    ThisThursday,
    ThisFriday,
    ThisSaturday,
    ThisJanuary,
    ThisFebuary,
    ThisMarch,
    ThisApril,
    ThisMay,
    ThisJune,
    ThisJuly,
    ThisAugust,
    ThisSeptember,
    ThisOctober,
    ThisNovember,
    ThisDecember,
    LastSunday,
    LastMonday,
    LastTuesday,
    LastWednesday,
    LastThursday,
    LastFriday,
    LastSaturday,
    LastWeek,
    LastMonth,
    LastYear,
    LastJanuary,
    LastFebuary,
    LastMarch,
    LastApril,
    LastMay,
    LastJune,
    LastJuly,
    LastAugust,
    LastSeptember,
    LastOctober,
    LastNovember,
    LastDecember,
    NextYear,
    NextMonth,
    NextWeek
}

public enum CountableDates
{
    Seconds,
    Minutes,
    Hours,
    Weeks,
    Months,
    Years
}

public interface IDateQueryable : IQueryable
{
    IDateQueryable Equal(DateTime date);

    IDateQueryable Equal(Dates date);

    IDateQueryable Before(DateTime date);

    IDateQueryable Before(Dates date);

    IDateQueryable After(DateTime date);

    IDateQueryable After(Dates date);

    IDateQueryable Between(DateTime from, DateTime to);

    IDateQueryable Between(Dates from, Dates to);

    IDateQueryable Last(int count, CountableDates date);

    IDateQueryable Next(int count, CountableDates date);
}
﻿using System;
using System.Collections.Generic;
using EverythingNet.Core;
using EverythingNet.Interfaces;

namespace EverythingNet.Query;

internal class DateQueryable : Queryable, IDateQueryable
{
    private string searchPattern;

    internal DateQueryable(IEverythingInternal everything, IQueryGenerator parent, string kind)
      : base(everything, parent)
    {
        this.searchPattern = kind;
        EverythingWrapper.FileInfoIndex fileInfoIndex;

        switch (this.searchPattern)
        {
            default:
                this.Flags = RequestFlags.EVERYTHING_REQUEST_DATE_MODIFIED;
                fileInfoIndex = EverythingWrapper.FileInfoIndex.DateModified;
                break;
            case "dc":
                this.Flags = RequestFlags.EVERYTHING_REQUEST_DATE_CREATED;
                fileInfoIndex = EverythingWrapper.FileInfoIndex.DateCreated;
                break;
            case "dr":
                this.Flags = RequestFlags.EVERYTHING_REQUEST_DATE_RUN;
                fileInfoIndex = EverythingWrapper.FileInfoIndex.DateAccessed;
                break;
            case "da":
                this.Flags = RequestFlags.EVERYTHING_REQUEST_DATE_ACCESSED;
                fileInfoIndex = EverythingWrapper.FileInfoIndex.DateAccessed;
                break;
        }

        this.IsFast = EverythingWrapper.Everything_IsFileInfoIndexed(fileInfoIndex);
    }

    public IDateQueryable Before(DateTime date)
    {
        return this.DateSearch($"<{date.ToShortDateString()}");
    }

    public IDateQueryable After(DateTime date)
    {
        return this.DateSearch($">{date.ToShortDateString()}");
    }

    public IDateQueryable Equal(DateTime date)
    {
        return this.DateSearch($"={date.ToShortDateString()}");
    }

    public IDateQueryable Between(DateTime from, DateTime to)
    {
        return this.DateSearch($"{from.ToShortDateString()}-{to.ToShortDateString()}");
    }

    public IDateQueryable Before(Dates date)
    {
        return this.DateSearch($"<{date}");
    }

    public IDateQueryable After(Dates date)
    {
        return this.DateSearch($">{date}");
    }

    public IDateQueryable Equal(Dates date)
    {
        return this.DateSearch($"{date}");
    }

    public IDateQueryable Between(Dates from, Dates to)
    {
        return this.DateSearch($"{from}-{to}");
    }

    public IDateQueryable Last(int count, CountableDates date)
    {
        return this.DateSearch($"last{count}{date}");
    }

    public IDateQueryable Next(int count, CountableDates date)
    {
        return this.DateSearch($"next{count}{date}");
    }

    public override IEnumerable<string> GetQueryParts()
    {
        foreach (var queryPart in base.GetQueryParts())
        {
            yield return queryPart;
        }

        yield return this.searchPattern;
    }

    private IDateQueryable DateSearch(string pattern)
    {
        this.searchPattern += pattern;

        return this;
    }
}
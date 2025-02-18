﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using EverythingNet.Core;
using EverythingNet.Interfaces;
using EverythingNet.Query;

namespace EverythingNet.Bench;


[SimpleJob(RunStrategy.ColdStart, launchCount: 2, warmupCount: 1, iterationCount: 4, invocationCount: 8, id: "FastAndDirtyJob")]
public class CombinedQueryBench
{
    [Benchmark]
    public long NameAndDateModified()
    {
        var queryable = new Everything()
            .Search()
            .Name.Contains("windows")
            .And
            .ModificationDate.Equal(Dates.Today);

        return queryable.Count;
    }

    [Benchmark]
    public long NameAndSize()
    {
        var queryable = new Everything()
            .Search()
            .Name.Contains("windows")
            .And
            .Size.LessThan(1, SizeUnit.Mb);

        return queryable.Count;
    }

    [Benchmark]
    public long QueryAndQuery()
    {
        var everything = new Everything();
        var queryable1 = everything
          .Search()
          .Name.Contains("windows");

        var queryable2 = everything
          .Search()
          .Size.LessThan(1, SizeUnit.Mb);

        return everything
          .Search()
          .Queryable(queryable1)
          .And
          .Queryable(queryable2)
          .Count;
    }
}
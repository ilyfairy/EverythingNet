using System.Collections.Generic;

namespace EverythingNet.Interfaces;

public interface IQueryable : IEnumerable<ISearchResult>
{
    bool IsFast { get; }

    long Count { get; }

    IQuery And { get; }

    IQuery Or { get; }
}
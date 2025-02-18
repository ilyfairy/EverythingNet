using System.Collections.Generic;

namespace EverythingNet.Interfaces;

public interface INameQueryable : IQueryable
{
    INameQueryable Contains(string pattern);

    INameQueryable StartWith(string pattern);

    INameQueryable EndWith(string pattern);

    INameQueryable Extension(string extension);

    INameQueryable Extensions(IEnumerable<string> extensions);

    INameQueryable Extensions(params string[] extensions);
}
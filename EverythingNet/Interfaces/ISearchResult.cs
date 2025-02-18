using System;

namespace EverythingNet.Interfaces;

public interface ISearchResult
{
    long Index { get; }

    bool IsFile { get; }

    string FullPath { get; }

    string Path { get; }

    string FileName { get; }

    long Size { get; }

    uint Attributes { get; }

    DateTime Created { get; }

    DateTime Modified { get; }

    DateTime Accessed { get; }

    DateTime Executed { get; }

    Exception LastException { get; }
}
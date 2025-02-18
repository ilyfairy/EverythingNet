﻿using System;
using System.Collections.Generic;

namespace EverythingNet.Interfaces;


public enum ErrorCode
{
    Ok = 0,
    Memory,
    Ipc,
    RegisterClassEX,
    CreateWindow,
    CreateThread,
    InvalidIndex,
    Invalidcall
}

[Flags]
public enum RequestFlags
{
    EVERYTHING_REQUEST_FILE_NAME = 0x00000001,
    EVERYTHING_REQUEST_PATH = 0x00000002,
    EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME = 0x00000004,
    EVERYTHING_REQUEST_EXTENSION = 0x00000008,
    EVERYTHING_REQUEST_SIZE = 0x00000010,
    EVERYTHING_REQUEST_DATE_CREATED = 0x00000020,
    EVERYTHING_REQUEST_DATE_MODIFIED = 0x00000040,
    EVERYTHING_REQUEST_DATE_ACCESSED = 0x00000080,
    EVERYTHING_REQUEST_ATTRIBUTES = 0x00000100,
    EVERYTHING_REQUEST_FILE_LIST_FILE_NAME = 0x00000200,
    EVERYTHING_REQUEST_RUN_COUNT = 0x00000400,
    EVERYTHING_REQUEST_DATE_RUN = 0x00000800,
    EVERYTHING_REQUEST_DATE_RECENTLY_CHANGED = 0x00001000,
    EVERYTHING_REQUEST_HIGHLIGHTED_FILE_NAME = 0x00002000,
    EVERYTHING_REQUEST_HIGHLIGHTED_PATH = 0x00004000,
    EVERYTHING_REQUEST_HIGHLIGHTED_FULL_PATH_AND_FILE_NAME = 0x00008000
}

public interface IEverything
{
    ResultKind ResulKind { get; set; }

    bool MatchCase { get; set; }

    bool MatchPath { get; set; }

    bool MatchWholeWord { get; set; }

    SortingKey SortKey { get; set; }

    IQuery Search();

    void Reset();
}

internal interface IEverythingInternal : IEverything
{
    long Count { get; }

    IEnumerable<ISearchResult> SendSearch(string searchPattern, RequestFlags flags);
}
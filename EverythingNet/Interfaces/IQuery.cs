﻿namespace EverythingNet.Interfaces;

public interface IQuery
{
    IQuery Not { get; }

    IQuery Files { get; }

    IQuery Folders { get; }

    IQuery NoSubFolder { get; }

    INameQueryable Name { get; }

    ISizeQueryable Size { get; }

    IDateQueryable CreationDate { get; }

    IDateQueryable ModificationDate { get; }

    IDateQueryable AccessDate { get; }

    IDateQueryable RunDate { get; }

    IMusicQueryable Music { get; }

    IFileQueryable File { get; }

    IImageQueryable Image { get; }

    IQueryable Queryable(IQueryable queryable);
}
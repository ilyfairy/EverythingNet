﻿using System.Collections.Generic;
using System.Linq;
using EverythingNet.Interfaces;
using IQueryable = EverythingNet.Interfaces.IQueryable;

namespace EverythingNet.Query;

internal class Query : IQuery, IQueryGenerator
{
    private readonly IEverythingInternal everything;
    private readonly IQueryGenerator parent;

    public Query(IEverythingInternal everything, IQueryGenerator parent = null)
    {
        this.everything = everything;
        this.parent = parent;
    }

    public IQuery Not => new LogicalQuery(this.everything, this, "!");

    public IQuery Files => new LogicalQuery(this.everything, this, "files:");

    public IQuery Folders => new LogicalQuery(this.everything, this, "folders:");

    public IQuery NoSubFolder => new LogicalQuery(this.everything, this, "nosubfolders:");

    public INameQueryable Name => new NameQueryable(this.everything, this);

    public ISizeQueryable Size => new SizeQueryable(this.everything, this);

    public IDateQueryable CreationDate => new DateQueryable(this.everything, this, "dc:");

    public IDateQueryable ModificationDate => new DateQueryable(this.everything, this, "dm:");

    public IDateQueryable AccessDate => new DateQueryable(this.everything, this, "da:");

    public IDateQueryable RunDate => new DateQueryable(this.everything, this, "dr:");

    public IMusicQueryable Music => new MusicQueryable(this.everything, this);

    public IFileQueryable File => new FileQueryable(this.everything, this);

    public IImageQueryable Image => new ImageQueryable(this.everything, this);

    public IQueryable Queryable(IQueryable queryable)
    {
        ((Queryable)queryable).SetParent(this);

        return queryable;
    }

    public RequestFlags Flags => this.parent?.Flags ?? 0;

    public virtual IEnumerable<string> GetQueryParts()
    {
        return this.parent?.GetQueryParts() ?? Enumerable.Empty<string>();
    }
}
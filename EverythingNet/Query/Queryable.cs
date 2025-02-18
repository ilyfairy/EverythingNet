﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EverythingNet.Interfaces;
using IQueryable = EverythingNet.Interfaces.IQueryable;

namespace EverythingNet.Query;

internal abstract class Queryable : IQueryable, IQueryGenerator
{
    private readonly IEverythingInternal everything;
    private IQueryGenerator parent;
    private IEnumerable<ISearchResult> results;

    protected Queryable(IEverythingInternal everything, IQueryGenerator parent)
    {
        this.everything = everything;
        this.parent = parent;
        this.IsFast = true;
    }

    public bool IsFast { get; protected set; }

    public long Count
    {
        get
        {
            this.ExecuteIfNeeded();

            return this.everything.Count;
        }
    }

    public IQuery And => new LogicalQuery(this.everything, this, " ");

    public IQuery Or => new LogicalQuery(this.everything, this, "|");

    public RequestFlags Flags { get; protected set; }

    public override string ToString()
    {
        return string.Concat(GetQueryParts());
    }

    public IEnumerator<ISearchResult> GetEnumerator()
    {
        this.ExecuteIfNeeded();

        return this.results.GetEnumerator();
    }

    public virtual IEnumerable<string> GetQueryParts()
    {
        return this.parent?.GetQueryParts() ?? Enumerable.Empty<string>();
    }

    protected string QuoteIfNeeded(string text)
    {
        if (text == null)
        {
            return string.Empty;
        }

        if (text.Contains(' ') && text.First() != '\"' && text.Last() != '\"')
        {
            return $"\"{text}\"";
        }

        return text;
    }

    internal void SetParent(IQueryGenerator onTheFlyparent)
    {
        this.parent = onTheFlyparent;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private void ExecuteIfNeeded()
    {
        if (this.results == null)
        {
            this.results = this.everything.SendSearch(string.Concat(GetQueryParts()), this.parent.Flags | this.Flags);
        }
    }
}
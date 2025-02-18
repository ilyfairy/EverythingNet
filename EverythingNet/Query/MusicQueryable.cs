﻿using System.Collections.Generic;
using EverythingNet.Interfaces;

namespace EverythingNet.Query;

internal class MusicQueryable : Queryable, IMusicQueryable
{
    private string artistPattern;
    private string genrePattern;
    private string titlePattern;
    private int? trackNumber;
    private string commentPattern;
    private string albumPattern;

    public MusicQueryable(IEverythingInternal everything, IQueryGenerator parent)
      : base(everything, parent)
    {
    }

    public IMusicQueryable Album(string album)
    {
        this.albumPattern = this.QuoteIfNeeded(album);

        return this;
    }

    public IMusicQueryable Artist(string artist)
    {
        this.artistPattern = this.QuoteIfNeeded(artist);

        return this;
    }

    public IMusicQueryable Genre(string genre)
    {
        this.genrePattern = this.QuoteIfNeeded(genre);

        return this;
    }

    public IMusicQueryable Title(string title)
    {
        this.titlePattern = this.QuoteIfNeeded(title);

        return this;
    }

    public IMusicQueryable Track(int? track)
    {
        this.trackNumber = track;

        return this;
    }

    public IMusicQueryable Comment(string comment)
    {
        if (!string.IsNullOrEmpty(comment))
        {
            this.commentPattern = this.QuoteIfNeeded(comment);
        }

        return this;
    }

    public override IEnumerable<string> GetQueryParts()
    {
        foreach (var queryPart in base.GetQueryParts())
        {
            yield return queryPart;
        }

        if (!string.IsNullOrEmpty(this.albumPattern))
        {
            yield return $"album:{this.albumPattern}";
        }

        if (!string.IsNullOrEmpty(this.artistPattern))
        {
            yield return $"artist:{this.artistPattern}";
        }

        if (!string.IsNullOrEmpty(this.genrePattern))
        {
            yield return $"genre:{this.genrePattern}";
        }

        if (!string.IsNullOrEmpty(this.titlePattern))
        {
            yield return $"title:{this.titlePattern}";
        }

        if (!string.IsNullOrEmpty(this.commentPattern))
        {
            yield return $"comment:{this.commentPattern}";
        }

        if (this.trackNumber.HasValue)
        {
            yield return $"track:{this.trackNumber}";
        }
    }
}
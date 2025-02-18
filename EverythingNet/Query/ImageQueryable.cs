﻿using System.Collections.Generic;
using EverythingNet.Interfaces;

namespace EverythingNet.Query;

internal class ImageQueryable : Queryable, IImageQueryable
{
    private string pattern;

    public ImageQueryable(IEverythingInternal everything, IQueryGenerator parent)
      : base(everything, parent)
    {
    }

    public IImageQueryable Width(int width)
    {
        return this.Search($"width:{width}");
    }

    public IImageQueryable Height(int height)
    {
        return this.Search($"height:{height}");
    }

    public IImageQueryable Portrait()
    {
        return this.Search("orienation:portrait");
    }

    public IImageQueryable Landscape()
    {
        return this.Search("orienation:landscape");
    }

    public IImageQueryable BitDepth(Bpp bpp)
    {
        return this.Search($"bitdepth:{(int)bpp}");
    }

    public override IEnumerable<string> GetQueryParts()
    {
        foreach (var queryPart in base.GetQueryParts())
        {
            yield return queryPart;
        }

        yield return this.pattern;
    }

    private IImageQueryable Search(string search)
    {
        this.pattern = search;

        return this;
    }
}
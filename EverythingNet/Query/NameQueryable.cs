using System;
using System.Collections.Generic;
using System.Linq;
using EverythingNet.Interfaces;

namespace EverythingNet.Query;

internal class NameQueryable : Queryable, INameQueryable
{
    private string pattern;
    private string startWith;
    private string endWith;
    private string extensions;

    public NameQueryable(IEverythingInternal everything, IQueryGenerator parent)
      : base(everything, parent)
    {
        this.Flags = RequestFlags.EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME;
    }

    public INameQueryable Contains(string contains)
    {
        this.pattern = this.QuoteIfNeeded(contains);

        return this;
    }

    public override IEnumerable<string> GetQueryParts()
    {
        foreach (var queryPart in base.GetQueryParts())
        {
            yield return queryPart;
        }

        if (!string.IsNullOrEmpty(this.startWith))
        {
            yield return $"startwith:{this.startWith}";
        }

        if (!string.IsNullOrEmpty(this.pattern))
        {
            yield return this.pattern;
        }

        if (!string.IsNullOrEmpty(this.endWith))
        {
            yield return $"endwith:{this.endWith}";
        }

        if (!string.IsNullOrEmpty(this.extensions))
        {
            yield return $"ext:{this.extensions}";
        }
    }

    public INameQueryable StartWith(string pattern)
    {
        this.startWith = this.QuoteIfNeeded(pattern);

        return this;
    }

    public INameQueryable EndWith(string pattern)
    {
        this.endWith = this.QuoteIfNeeded(pattern);

        return this;
    }

    public INameQueryable Extension(string extension)
    {
        if (extension.Contains("."))
        {
            throw new ArgumentException("Do not specify the dot character when specifying an extension");
        }

        this.extensions = string.IsNullOrEmpty(this.extensions)
          ? extension
          : $"{this.extensions};{extension}";

        return this;
    }

    public INameQueryable Extensions(IEnumerable<string> newExtensions)
    {
        return this.ExtensionCollection(newExtensions);
    }

    public INameQueryable Extensions(params string[] newExtensions)
    {
        return this.ExtensionCollection(newExtensions);
    }

    private INameQueryable ExtensionCollection(IEnumerable<string> newExtensions)
    {
        if (newExtensions == null)
        {
            throw new ArgumentNullException(nameof(newExtensions));
        }

        if (!newExtensions.Any())
        {
            throw new ArgumentException("The list of exceptions must not be empty");
        }

        if (newExtensions.Any(x => x.Contains(".")))
        {
            throw new ArgumentException("Do not specify the dot character when specifying an extension");
        }

        return this.Extension(string.Join(";", newExtensions));
    }
}
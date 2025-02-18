using System;
using System.IO;
using System.Text;
using EverythingNet.Interfaces;

namespace EverythingNet.Core;

internal class SearchResult : ISearchResult
{
    private delegate bool MyDelegate(uint index, out long date);

    private readonly uint replyId;
    private readonly uint index;

    private string fullPath;

    public SearchResult(int index, uint replyId)
    {
        this.replyId = replyId;
        this.index = Convert.ToUInt32(index);
    }

    public long Index => this.index;

    public bool IsFile => EverythingWrapper.Everything_IsFileResult(this.index);

    public string FullPath
    {
        get
        {
            if (this.fullPath == null)
            {
                var builder = new StringBuilder(260);

                EverythingWrapper.Everything_SetReplyID(this.replyId);
                EverythingWrapper.Everything_GetResultFullPathName(this.index, builder, 260);

                this.fullPath = builder.ToString();
            }

            return this.fullPath;
        }
    }

    public string Path
    {
        get
        {
            //EverythingWrapper.Everything_SetReplyID(this.replyId);
            //return EverythingWrapper.Everything_GetResultPath(this.index);

            // Temporary implementation until the native function works as expected
            try
            {
                return !string.IsNullOrEmpty(this.FullPath)
                  ? System.IO.Path.GetDirectoryName(this.FullPath)
                  : string.Empty;
            }
            catch (Exception e)
            {
                this.LastException = e;

                return this.FullPath;
            }
        }
    }

    public string FileName
    {
        get
        {
            //EverythingWrapper.Everything_SetReplyID(this.replyId);
            //return EverythingWrapper.Everything_GetResultFileName(this.index);

            // Temporary implementation until the native function works as expected
            try
            {
                return !string.IsNullOrEmpty(this.FullPath)
                  ? System.IO.Path.GetFileName(this.FullPath)
                  : string.Empty;
            }
            catch (Exception e)
            {
                this.LastException = e;

                return this.FullPath;
            }
        }
    }

    public long Size
    {
        get
        {
            EverythingWrapper.Everything_SetReplyID(this.replyId);
            EverythingWrapper.Everything_GetResultSize(this.index, out var size);

            return size;
        }
    }

    public uint Attributes
    {
        get
        {
            EverythingWrapper.Everything_SetReplyID(this.replyId);
            uint attributes = EverythingWrapper.Everything_GetResultAttributes(this.index);

            return attributes > 0
              ? attributes
              : (!string.IsNullOrEmpty(this.FullPath)
                ? (uint)File.GetAttributes(this.FullPath)
                : 0);
        }
    }

    public DateTime Created => this.GenericDate(EverythingWrapper.Everything_GetResultDateCreated, File.GetCreationTime);

    public DateTime Modified => this.GenericDate(EverythingWrapper.Everything_GetResultDateModified, File.GetLastWriteTime);

    public DateTime Accessed => this.GenericDate(EverythingWrapper.Everything_GetResultDateAccessed, File.GetLastAccessTime);

    public DateTime Executed => this.GenericDate(EverythingWrapper.Everything_GetResultDateRun, File.GetLastAccessTime);

    public Exception LastException { get; private set; }

    private DateTime GenericDate(MyDelegate func, Func<string, DateTime> fallbackDelegate)
    {
        EverythingWrapper.Everything_SetReplyID(this.replyId);
        if (func(this.index, out var date) && date >= 0)
        {
            return DateTime.FromFileTime(date);
        }

        return !string.IsNullOrEmpty(this.FullPath)
          ? fallbackDelegate(this.FullPath)
          : DateTime.MinValue;
    }
}
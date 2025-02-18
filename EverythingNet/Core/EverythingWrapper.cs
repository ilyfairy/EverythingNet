﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace EverythingNet.Core;

internal class EverythingWrapper
{
    private static readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

    private class Locker : IDisposable
    {
        private readonly ReaderWriterLockSlim locker;

        public Locker(ReaderWriterLockSlim locker)
        {
            this.locker = locker;
            this.locker.EnterWriteLock();
        }

        public void Dispose()
        {
            this.locker.ExitWriteLock();
        }
    }

#if WIN32
private const string EverythingDLL = EverythingDLL;
#else
    private const string EverythingDLL = "Everything64.dll";
#endif

    private const int EVERYTHING_OK = 0;
    private const int EVERYTHING_ERROR_MEMORY = 1;
    private const int EVERYTHING_ERROR_IPC = 2;
    private const int EVERYTHING_ERROR_REGISTERCLASSEX = 3;
    private const int EVERYTHING_ERROR_CREATEWINDOW = 4;
    private const int EVERYTHING_ERROR_CREATETHREAD = 5;
    private const int EVERYTHING_ERROR_INVALIDINDEX = 6;
    private const int EVERYTHING_ERROR_INVALIDCALL = 7;

    public enum FileInfoIndex
    {
        FileSize = 1,
        FolderSize,
        DateCreated,
        DateModified,
        DateAccessed,
        Attributes
    }


    internal static IDisposable Lock()
    {
        return new Locker(locker);
    }

    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsDBLoaded();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetMajorVersion();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetMinorVersion();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetRevision();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetBuildNumber();

    [DllImport(EverythingDLL)]
    public static extern int Everything_SetSearch(string lpSearchString);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetMatchPath(bool bEnable);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetMatchCase(bool bEnable);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetMatchWholeWord(bool bEnable);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetRegex(bool bEnable);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetMax(UInt32 dwMax);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetOffset(UInt32 dwOffset);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetReplyWindow(IntPtr handler);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetReplyID(UInt32 nId);

    [DllImport(EverythingDLL)]
    public static extern void Everything_Reset();

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetMatchPath();

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetMatchCase();

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetMatchWholeWord();

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetRegex();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetMax();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetOffset();

    [DllImport(EverythingDLL)]
    public static extern IntPtr Everything_GetSearch();

    [DllImport(EverythingDLL)]
    public static extern int Everything_GetLastError();

    [DllImport(EverythingDLL)]
    public static extern bool Everything_Query(bool bWait);

    [DllImport(EverythingDLL)]
    public static extern void Everything_SortResultsByPath();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetNumFileResults();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetNumFolderResults();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetNumResults();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetTotFileResults();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetTotFolderResults();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetTotResults();

    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsVolumeResult(UInt32 nIndex);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsFolderResult(UInt32 nIndex);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsFileResult(UInt32 nIndex);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern void Everything_GetResultFullPathName(UInt32 nIndex, StringBuilder lpString, UInt32 nMaxCount);

    // Everything 1.4
    [DllImport(EverythingDLL)]
    public static extern void Everything_SetSort(UInt32 dwSortType);


    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetSort();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetResultListSort();

    [DllImport(EverythingDLL)]
    public static extern void Everything_SetRequestFlags(UInt32 dwRequestFlags);

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetRequestFlags();

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetResultListRequestFlags();

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern string Everything_GetResultExtension(UInt32 nIndex);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetResultSize(UInt32 nIndex, out long lpFileSize);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetResultDateCreated(UInt32 nIndex, out long lpFileTime);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetResultDateModified(UInt32 nIndex, out long lpFileTime);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetResultDateAccessed(UInt32 nIndex, out long lpFileTime);

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetResultAttributes(UInt32 nIndex);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern string Everything_GetResultFileListFileName(UInt32 nIndex);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern string Everything_GetResultPath(UInt32 nIndex);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern string Everything_GetResultFileName(UInt32 nIndex);

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetResultRunCount(UInt32 nIndex);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetResultDateRun(UInt32 nIndex, out long lpFileTime);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_GetResultDateRecentlyChanged(UInt32 nIndex, out long lpFileTime);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern string Everything_GetResultHighlightedFileName(UInt32 nIndex);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern string Everything_GetResultHighlightedPath(UInt32 nIndex);

    [DllImport(EverythingDLL, CharSet = CharSet.Unicode)]
    public static extern string Everything_GetResultHighlightedFullPathAndFileName(UInt32 nIndex);

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_GetRunCountFromFileName(string lpFileName);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_SetRunCountFromFileName(string lpFileName, UInt32 dwRunCount);

    [DllImport(EverythingDLL)]
    public static extern UInt32 Everything_IncRunCountFromFileName(string lpFileName);

    [DllImport(EverythingDLL)]
    public static extern bool Everything_IsFileInfoIndexed(FileInfoIndex fileInfoType);
}
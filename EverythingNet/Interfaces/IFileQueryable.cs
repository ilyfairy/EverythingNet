﻿namespace EverythingNet.Interfaces;

public interface IFileQueryable : IQueryable
{
    IFileQueryable Roots();

    IFileQueryable Parent(string parentFolder);

    IFileQueryable Audio(string search = null);

    IFileQueryable Zip(string search = null);

    IFileQueryable Video(string search = null);

    IFileQueryable Picture(string search = null);

    IFileQueryable Exe(string search = null);

    IFileQueryable Document(string search = null);

    IFileQueryable Duplicates(string search = null);
}
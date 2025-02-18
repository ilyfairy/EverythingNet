﻿namespace EverythingNet.Interfaces;

public interface IMusicQueryable : IQueryable
{
    IMusicQueryable Album(string album);

    IMusicQueryable Artist(string artist);

    IMusicQueryable Genre(string genre);

    IMusicQueryable Title(string title);

    // TODO: Add a way to nicely support constraints on track value (<, >, between)
    IMusicQueryable Track(int? track);

    IMusicQueryable Comment(string comment);
}
﻿namespace EverythingNet.Interfaces;

public enum Bpp
{
    Bpp1 = 1,
    Bpp8 = 8,
    Bpp16 = 16,
    Bpp24 = 24,
    Bpp32 = 32
}

/// <summary>
///   Only jpg, png, gif and bmp file are supported with these queries
/// </summary>
public interface IImageQueryable : IQueryable
{
    IImageQueryable Width(int width);

    IImageQueryable Height(int height);

    IImageQueryable Portrait();

    IImageQueryable Landscape();

    IImageQueryable BitDepth(Bpp bpp);
}
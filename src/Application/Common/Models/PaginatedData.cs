// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcosferaBlazor.Auth.Application.Common.Models;

public class PaginatedData<T>
{
    public int CurrentPage { get; private set; }
    public int TotalItems { get; private set; }
    public int TotalPages { get; private set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public IEnumerable<T> Items { get; set; }
    public PaginatedData(IEnumerable<T> items, int total,int pageIndex,int pageSize)
    {
        Items = items;
        TotalItems = total;
        CurrentPage = pageIndex;
        TotalPages = (int)Math.Ceiling(total / (double)pageSize);
    }
    public static async Task<PaginatedData<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedData<T>(items, count, pageIndex, pageSize);
    }
}

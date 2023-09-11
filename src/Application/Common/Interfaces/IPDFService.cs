// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcosferaBlazor.Auth.Application.Common.Interfaces;

public interface IPDFService
{
    Task<byte[]> ExportAsync<TData>(IEnumerable<TData> data
        , Dictionary<string, Func<TData, object?>> mappers
        , string title, bool landscape);

}

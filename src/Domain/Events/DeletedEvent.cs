// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace EcosferaBlazor.Auth.Domain.Events;
public class DeletedEvent<T> : DomainEvent where T : IEntity
{
    public DeletedEvent(T entity)
    {
        Entity = entity;
    }

    public T Entity { get; }
}

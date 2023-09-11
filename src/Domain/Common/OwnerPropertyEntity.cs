﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;
using EcosferaBlazor.Auth.Domain.Identity;

namespace EcosferaBlazor.Auth.Domain.Common;

public abstract class OwnerPropertyEntity: BaseAuditableEntity
{
    [ForeignKey("CreatedBy")]
    public virtual ApplicationUser? Owner { get; set; }
    [ForeignKey("LastModifiedBy")]
    public virtual ApplicationUser? Editor { get; set; }
}

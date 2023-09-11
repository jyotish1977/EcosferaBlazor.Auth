// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcosferaBlazor.Auth.Application.Features.Tenants.Commands.AddEdit;

public class AddEditTenantCommandValidator : AbstractValidator<AddEditTenantCommand>
{
    public AddEditTenantCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(256)
            .NotEmpty();
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(ValidationContext<AddEditTenantCommand>.CreateWithOptions((AddEditTenantCommand)model,
                x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}
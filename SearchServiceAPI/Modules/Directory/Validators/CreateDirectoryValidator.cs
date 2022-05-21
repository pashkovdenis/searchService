using FastEndpoints;
using FluentValidation;
using SearchServiceAPI.Modules.Directory.Request;

namespace SearchServiceAPI.Modules.Directory.Validators;

/// <summary>
/// Validate create directory request
/// </summary>
public sealed class CreateDirectoryValidator : Validator<CreateDirectoryRequest>
{
    public CreateDirectoryValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Client token is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Directory nae is required");
        RuleFor(x => x.LayerTemplates).NotEmpty();
    }
}
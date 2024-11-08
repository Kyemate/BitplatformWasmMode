﻿using BitplatformWasmMode.Client.Core;

namespace BitplatformWasmMode.Server.Web;

public partial class ServerWebAppSettings : ClientAppSettings
{
    public ForwardedHeadersOptions ForwardedHeaders { get; set; } = default!;

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = base.Validate(validationContext).ToList();

        Validator.TryValidateObject(ForwardedHeaders, new ValidationContext(ForwardedHeaders), validationResults, true);

        return validationResults;
    }
}

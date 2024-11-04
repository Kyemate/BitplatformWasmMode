using Riok.Mapperly.Abstractions;
using BitplatformWasmMode.Server.Api.Models.Identity;
using BitplatformWasmMode.Shared.Dtos.Identity;

namespace BitplatformWasmMode.Server.Api.Mappers;

/// <summary>
/// More info at Server/Mappers/README.md
/// </summary>
[Mapper(UseDeepCloning = true)]
public static partial class IdentityMapper
{
    public static partial UserDto Map(this User source);
    public static partial void Patch(this EditUserDto source, User destination);
    public static partial UserSessionDto Map(this UserSession source);
}

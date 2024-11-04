using BitplatformWasmMode.Server.Api.Models.Identity;

namespace BitplatformWasmMode.Server.Api.Services.Identity;

public partial class AppUserConfirmation : IUserConfirmation<User>
{
    public async Task<bool> IsConfirmedAsync(UserManager<User> manager, User user)
    {
        return user.EmailConfirmed || user.PhoneNumberConfirmed;
    }
}

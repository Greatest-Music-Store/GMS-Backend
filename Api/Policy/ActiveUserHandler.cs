using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using GMS_Backend.Application.Services;
using GMS_Backend.Domain.Enums;

namespace GMS_Backend.Api.Policy;

public class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
{
    private readonly UserService _userService;

    public ActiveUserHandler(UserService userService)
    {
        _userService = userService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ActiveUserRequirement requirement)
    {
        var claim = context.User.FindFirst(ClaimTypes.NameIdentifier);

        if (claim == null)
            return;

        var user = await _userService.GetByIdAsync(Guid.Parse(claim.Value));

        if (user == null)
            return;

        if (user.Status == UserStatus.Active)
        {
            context.Succeed(requirement);
        }
    }
}
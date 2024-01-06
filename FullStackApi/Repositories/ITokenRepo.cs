using Microsoft.AspNetCore.Identity;

namespace FullStackApi.Repositories
{
    public interface ITokenRepo
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}

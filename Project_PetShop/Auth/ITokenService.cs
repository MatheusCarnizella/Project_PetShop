using Project_PetShop.Models;

namespace Project_PetShop.Auth
{
    public interface ITokenService
    {
        string GetToken(string key, string issuer, string audience, Usuario user);
    }
}

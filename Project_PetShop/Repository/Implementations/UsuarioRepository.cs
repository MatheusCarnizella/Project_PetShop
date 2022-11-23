using Project_PetShop.Context;
using Project_PetShop.Models;

namespace Project_PetShop.Repository.Implementations
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }
    }
}

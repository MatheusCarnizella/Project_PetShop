using Project_PetShop.Context;
using Project_PetShop.Models;

namespace Project_PetShop.Repository.Implementations
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }
    }
}

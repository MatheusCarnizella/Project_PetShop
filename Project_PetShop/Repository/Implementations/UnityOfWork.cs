using Project_PetShop.Context;

namespace Project_PetShop.Repository.Implementations;

public class UnityOfWork : IUnityOfWork
{
    private ProdutoRepository? _produtoRepository;
    private UsuarioRepository? _usuarioRepository;
    public AppDbContext _context;

    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
        }
    }

    public IUsuarioRepository UsuarioRepository
    {
        get
        {
            return _usuarioRepository = _usuarioRepository ?? new UsuarioRepository(_context);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

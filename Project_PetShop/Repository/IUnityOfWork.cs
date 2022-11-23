namespace Project_PetShop.Repository
{
    public interface IUnityOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }

        Task Save();
    }
}

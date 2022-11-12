using Project_PetShop.Interfaces;
using System.Text.Json.Serialization;

namespace Project_PetShop.Models
{
    public class Produto : IProduto
    {
        public int idProduto { get; set; }
        public string? nomeProduto { get; set; }
        public string? produtoDescricao { get; set; }
        public string? Valor { get; set; }
        public string? Quantidade { get; set; }
        public string? urlImagem { get; set; }

        [JsonIgnore]
        public int? Id { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        List<Produto> IProduto.GetProdutos()
        {
            var produtos = new List<Produto>();
            produtos.Add(new Produto { idProduto = idProduto, Id = Id });
            return produtos;
        }
    }
}

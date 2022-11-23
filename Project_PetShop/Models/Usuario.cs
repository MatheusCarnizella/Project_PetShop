using System.Text.Json.Serialization;

namespace Project_PetShop.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? userName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        [JsonIgnore]
        public string? Produto { get; set; }

        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
               
    }
}

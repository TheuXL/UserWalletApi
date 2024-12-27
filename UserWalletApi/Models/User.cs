namespace UserWalletApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public required string CPF { get; set; }
    }
}
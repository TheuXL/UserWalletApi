namespace UserWalletApi.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public decimal ValorAtual { get; set; }
        public required string Banco { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
    }
}
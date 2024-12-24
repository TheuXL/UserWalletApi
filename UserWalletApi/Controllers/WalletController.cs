using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserWalletApi.Models;
using UserWalletApi.Services;

namespace UserWalletApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<ActionResult<Wallet>> CreateWallet(Wallet wallet)
        {
            var createdWallet = await _walletService.AddWalletAsync(wallet);
            return CreatedAtAction(nameof(CreateWallet), new { id = createdWallet.Id }, createdWallet);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Wallet>>> GetWallets(int userId)
        {
            var wallets = await _walletService.GetWalletsByUserIdAsync(userId);
            return Ok(wallets ?? new List<Wallet>());  // Coalescência nula para garantir lista vazia em caso de null
        }
    }
}

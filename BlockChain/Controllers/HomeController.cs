using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlockChain.Models;
using BlockChain.Api;
//BlockChainMiner
namespace BlockChain.Controllers
{
    public class HomeController : Controller
    {
        private static CryptoCurrency blockchain = BlockChainController.blockchain; //new CryptoCurrency();

        public IActionResult Index()
        {
            List<Transaction> transactions = blockchain.GetTransactions();
            ViewBag.Block = transactions;

            List<Block> blocks = blockchain.GetBlocks();
            ViewBag.Blocks = blocks;
            
            return View(transactions);
        }

        public IActionResult Mine()
        {
            blockchain.Mine();
            return RedirectToAction("Index");
        }

        public IActionResult Configure()
        {
            return View(blockchain.GetNodes());
        }

        public IActionResult RegisterNodes(string nodes)
        {
            string[] node = nodes.Split(',');
            blockchain.RegisterNodes(node);
            return RedirectToAction("Configure");
        }


        public IActionResult CoinBase()
        {
            List<Block> blocks = blockchain.GetBlocks();
            ViewBag.Blocks = blocks;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

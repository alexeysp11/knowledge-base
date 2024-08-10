using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using KnowledgeBase.Examples.TicTacToe.Controllers;

namespace KnowledgeBase.Examples.TicTacToe.Controllers
{
    public class TicTacToeController : Controller
    {
        public string Index()
        {
            return "This is my default action...";
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Game()
        {
            return View();
        }

        public IActionResult TicTacToe_React()
        {
            return View();
        }
    }
}
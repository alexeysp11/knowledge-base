using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Concepts.Examples.TicTacToe.TicTacToeGame
{
    public class JsMethods
    {
        public static int userVariable = 45;
        
        [JSInvokable]
        public static Task<int> GenerateRandomInt()
        {
            return Task.FromResult(new Random().Next());
        }
    }
}
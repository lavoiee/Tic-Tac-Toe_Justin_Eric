using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class FeatureNotImplementedException : Exception
    {
        public FeatureNotImplementedException()
        {

        }

        public FeatureNotImplementedException(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
        }

        public FeatureNotImplementedException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}

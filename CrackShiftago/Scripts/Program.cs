using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackShiftago
{
    class Program
    {
        private static Game game;
        private static int roundCount = 0;

        public static Random random;

        static void Main(string[] args)
        {
            game = new Game();
            random = new Random();

            game.Initialize();
            
            while (game.ComputeLoop())
            {
                roundCount++;
                //Console.WriteLine(roundCount);
            }


            while (true)
                Console.Read();
        }
    }
}

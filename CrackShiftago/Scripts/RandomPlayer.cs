using System;

namespace CrackShiftago
{
    public class RandomPlayer : Player
    {
        public RandomPlayer(byte color) : base(color)
        {
        }

        public override int Turn(Board board)
        {
            int rndMove;
            do
            {
                rndMove = Program.random.Next(0, 28);
            }
            while (!board.TestForValidMove(rndMove));

            Console.Write("P" + color + "s turn: " + rndMove);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            board.InsertMarble(color, rndMove);

            if (board.CheckForPoints(color, ref score))
                return -1;

            if (score >= Game.pointsToWin)
                return 1;
            return 0;
        }
    }
}
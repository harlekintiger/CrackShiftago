using System;

namespace CrackShiftago
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(byte color) : base(color)
        {
        }

        /// <summary>
        /// Returns 1 when game is won, -1 when it's a draw, 0 otherwise
        /// </summary>
        public override int Turn(Board board)
        {
            string[] inputs;
            int arg0, arg1, arg2;

            //for (int i = 0; i < 50; i++)
            //{
            //    Console.WriteLine(random.Next(0, 28));
            //}

            while (true)
            {
                Console.Write("P" + color + "s turn: ");
                inputs = Console.ReadLine().Split(' ');

                if(inputs.Length == 1 && inputs[0].Equals(""))
                {
                    int rndMove = Program.random.Next(0, 28);

                    if (!board.TestForValidMove(rndMove))
                        continue;

                    Console.Write(rndMove);

                    Console.WriteLine();
                    Console.WriteLine();

                    board.InsertMarble(color, rndMove);
                    break;
                }

                if (inputs.Length == 1                  &&
                    int.TryParse(inputs[ 0 ], out arg0) &&
                    board.TestForValidMove(arg0)          )
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();

                    board.InsertMarble(color, arg0);
                    break;
                }
                
                if (inputs.Length == 3                       &&
                    int.TryParse(inputs[ 0 ], out arg0)      &&
                    int.TryParse(inputs[ 1 ], out arg1)      &&
                    int.TryParse(inputs[ 2 ], out arg2)      &&
                    board.TestForValidMove(arg0, arg1, arg2)   )
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();

                    board.InsertMarble(color, arg0, arg1, arg2);
                    break;
                }
            }

            if (board.CheckForPoints(color, ref score))
                return -1;

            if (score >= Game.pointsToWin)
                return 1;
            return 0;
        }
    }
}
﻿using System;

namespace CrackShiftago
{
    public class Game
    {
        public static readonly int pointsToWin = 10;

        private Player[] players;

        private Board board;

        public void Initialize()
        {
            board = new Board();

            players = new[]
            {
                new Player(1),
                new Player(2)
            };
        }

        public bool ComputeLoop()
        {
            foreach (Player player in players)
            {
                SetUp();
                int turnResult;
                if((turnResult = player.Turn(board)) != 0)
                {
                    if (turnResult == 1)
                        return GameWon(player);
                    if (turnResult == -1)
                        return GameDraw();
                }
            }
            return true;
        }

        private bool GameDraw()
        {
            if (players[ 0 ].score > players[ 1 ].score)
                return GameWon(players[ 0 ]);
            if (players[ 1 ].score > players[ 0 ].score)
                return GameWon(players[ 1 ]);

            Console.WriteLine("Game ended in a draw. To bad ");
            return false;
        }

        private bool GameWon(Player player)
        {
            Console.WriteLine("Player " + player.ToString() + " won!!! Congratulations!");
            Console.Read();
            return false;
        }

        private void SetUp()
        {
            Console.WriteLine(players[0]);
            Console.WriteLine(players[1]);
            //Console.Clear();
            board.Draw();
        }
    }
}
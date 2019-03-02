using System;
using System.Collections;
using points = System.Int32;
using length = System.Int32;

namespace CrackShiftago
{
    public class Board
    {
        private const int boardDimension = 7;       // Don't change either unless you understand how CheckForPoints() work.
        private const int minScorableRowLength = 5; // Don't change either unless you understand how CheckForPoints() work.
        private readonly Hashtable pointsForLength; // Use pointsForLength[length] to get the points
        public readonly InputVariation[] inputVariations;

        private byte[,] board;
        private byte[,] tempBoard;


        public Board()
        {
            board = new byte[ boardDimension, boardDimension ];
            tempBoard = new byte[ boardDimension, boardDimension ];
            
            /*
            board = new byte[,]{ { 0, 0, 0, 2, 0, 0, 0 },
                                 { 0, 1, 0, 2, 0, 0, 0 },
                                 { 0, 0, 1, 1, 0, 0, 0 },
                                 { 0, 0, 0, 1, 0, 0, 0 },
                                 { 0, 0, 0, 0, 0, 0, 0 },
                                 { 0, 0, 0, 2, 0, 0, 0 },
                                 { 0, 0, 0, 2, 0, 0, 0 } };
                                 */

            pointsForLength = new Hashtable()
            {
                {5 /*length*/,  2 /*points*/},
                {6 /*length*/,  5 /*points*/},
                {7 /*length*/, 10 /*points*/}
            };

            inputVariations = new[]{
                new InputVariation(0, 0,  0,  1),
                new InputVariation(1, 0,  0,  1),
                new InputVariation(2, 0,  0,  1),
                new InputVariation(3, 0,  0,  1),
                new InputVariation(4, 0,  0,  1),
                new InputVariation(5, 0,  0,  1),
                new InputVariation(6, 0,  0,  1),

                new InputVariation(6, 0, -1,  0),
                new InputVariation(6, 1, -1,  0),
                new InputVariation(6, 2, -1,  0),
                new InputVariation(6, 3, -1,  0),
                new InputVariation(6, 4, -1,  0),
                new InputVariation(6, 5, -1,  0),
                new InputVariation(6, 6, -1,  0),

                new InputVariation(6, 6,  0, -1),
                new InputVariation(5, 6,  0, -1),
                new InputVariation(4, 6,  0, -1),
                new InputVariation(3, 6,  0, -1),
                new InputVariation(2, 6,  0, -1),
                new InputVariation(1, 6,  0, -1),
                new InputVariation(0, 6,  0, -1),

                new InputVariation(0, 6,  1,  0),
                new InputVariation(0, 5,  1,  0),
                new InputVariation(0, 4,  1,  0),
                new InputVariation(0, 3,  1,  0),
                new InputVariation(0, 2,  1,  0),
                new InputVariation(0, 1,  1,  0),
                new InputVariation(0, 0,  1,  0)
            };
        }

        public bool TestForValidMove(int inputVariation)
        {
            return TestForValidMove(
                inputVariations[ inputVariation ].xStart,
                inputVariations[ inputVariation ].yStart,
                inputVariations[ inputVariation ].xDir,
                inputVariations[ inputVariation ].yDir   );
        }

        public bool TestForValidMove(int startingCellX, int startingCellY, int direction)
        {
            int xDir = (2 - direction    ) % 2; //1 = (1, 0), 2 = (0, 1), 3 = (-1, 0), 4 = (0, -1)
            int yDir = (2 - direction + 1) % 2; //1 = (1, 0), 2 = (0, 1), 3 = (-1, 0), 4 = (0, -1)
            return TestForValidMove(startingCellX, startingCellY, xDir, yDir);
        }

        public bool TestForValidMove(int startingCellX, int startingCellY, int xDir, int yDir)
        {
            for (int i = 0; i < boardDimension; i++)
            {
                if (board[ startingCellX + i * xDir, startingCellY + i * yDir ] == 0)
                    return true;
            }
            return false;
        }

        public void InsertMarble(byte newMarble, int inputVariation)
        {
            InsertMarble(
                newMarble,
                inputVariations[ inputVariation ].xStart,
                inputVariations[ inputVariation ].yStart,
                inputVariations[ inputVariation ].xDir,
                inputVariations[ inputVariation ].yDir   );
        }

        public void InsertMarble(byte newMarble, int startingCellX, int startingCellY, int direction)
        {
            int xDir = (2 - direction)     % 2; //1 = (1, 0), 2 = (0, 1), 3 = (-1, 0), 4 = (0, -1)
            int yDir = (2 - direction + 1) % 2; //1 = (1, 0), 2 = (0, 1), 3 = (-1, 0), 4 = (0, -1)
            InsertMarble(newMarble, startingCellX, startingCellY, xDir, yDir);
        }

        public void InsertMarble(byte newMarble, int startingCellX, int startingCellY, int xDir, int yDir)
        {
            Buffer.BlockCopy(board, 0, tempBoard, 0, board.Length);

            board[ startingCellX, startingCellY ] = newMarble;
            if (tempBoard[ startingCellX, startingCellY ] == 0)
                return;

            for (int i = 1; i < boardDimension; i++)
            {
                board[ startingCellX + i * xDir, startingCellY + i * yDir ] =
                    tempBoard[ startingCellX + (i - 1) * xDir, startingCellY + (i - 1) * yDir ];

                if (tempBoard[ startingCellX + i * xDir, startingCellY + i * yDir ] == 0)
                    return;
            }
        }

        /// <summary>
        /// Returns whether all spaces are filled (draw)
        /// </summary>
        public bool CheckForPoints(byte color, ref int currentScore)
        {
            Console.WriteLine("Checking for points.");
            int remainingZeros = 0;

            Buffer.BlockCopy(board, 0, tempBoard, 0, board.Length);

            for (int y = 0; y < boardDimension; y++)
            {
                for (int x = 0; x < boardDimension; x++)
                {
                    if (tempBoard[ x, y ] == color)
                        tempBoard[ x, y ] = 1;
                    else
                    {
                        if(tempBoard[x, y] == 0)
                            remainingZeros++;

                        tempBoard[ x, y ] = 0;
                    }
                }
            }

            for (int currVariationIndex = 0; currVariationIndex < inputVariations.Length / 2; currVariationIndex++)
            {
                InputVariation currVariation = inputVariations[ currVariationIndex ];

                byte[] currRow = new byte[boardDimension];
                int occurrenceCounter = 0;
                for (int i = 0; i < boardDimension; i++)
                {
                    if ((currRow[ i ] = tempBoard[ currVariation.xStart + i * currVariation.xDir, currVariation.yStart + i * currVariation.yDir ]) == 1)
                    {
                        occurrenceCounter++;
                    }
                    else
                    {
                        if (occurrenceCounter < minScorableRowLength)
                            occurrenceCounter = 0;
                        else
                            break;
                    }
                }

                if (occurrenceCounter < minScorableRowLength)
                    continue;

                for (int i = (boardDimension / 2) - 1; i > 0; i--)
                {
                    if (currRow[ i - 1 ] != 0)
                        currRow[ i ] = 8;
                    else
                        break;
                }
                for (int i = boardDimension / 2; i < boardDimension - 1; i++)
                {
                    if (currRow[ i + 1 ] != 0)
                        currRow[ i ] = 8;
                    else
                        break;
                }

                for (int i = 0; i < boardDimension; i++)
                {
                    if (currRow[ i ] == 8)
                        board[ currVariation.xStart + i * currVariation.xDir, currVariation.yStart + i * currVariation.yDir ] = 0;
                }

                currentScore += (int)pointsForLength[occurrenceCounter];
                return false;
            }

            if(remainingZeros / 2 < 2)
            {
                for (int y = 0; y < boardDimension; y++)
                    for (int x = 0; x < boardDimension; x++)
                        if (board[ x, y ] == 0)
                            return false;

                return true;
            }

            return false;
        }

        public void Draw()
        {
            for (int y = 0; y < boardDimension; y++)
            {
                for (int x = 0; x < boardDimension; x++)
                {
                    Console.Write(board[x, y] + " ");    
                }
                Console.WriteLine();
            }
        }

        private void DrawSpecific(byte[,] boardToDraw)
        {
            for (int y = 0; y < boardDimension; y++)
            {
                for (int x = 0; x < boardDimension; x++)
                {
                    Console.Write(boardToDraw[ x, y ] + " ");
                }
                Console.WriteLine("     spb");
            }
        }

        public struct InputVariation
        {
            public int xStart;
            public int yStart;
            public int xDir;
            public int yDir;

            public InputVariation(int xStart, int yStart, int xDir, int yDir)
            {
                this.xStart = xStart;
                this.yStart = yStart;
                this.xDir = xDir;
                this.yDir = yDir;
            }
        }

    }
}
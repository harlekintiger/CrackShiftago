using System;

namespace CrackShiftago
{
    public abstract class Player
    {
        protected byte color;

        protected int score;
        public int Score { get { return score; } }

        protected Player(byte color)
        {
            if (color == 0)
                throw new ArgumentException();

            this.color = color;
            score = 0;
        }

        public abstract int Turn(Board board);

        public override string ToString()
        {
            return "Color: " + color + "; Score: " + score;
        }
    }
}
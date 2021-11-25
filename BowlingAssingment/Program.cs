using BowlingAssingment.Bowling;
using System;

namespace BowlingAssingment
{
    public class Program
    {
        static void Main(string[] args)
        {

            int highestScore = 0;

            for (int i = 0; i < 1; i++)
            {
                int strikes = 0;
                int spares = 0;
                int score = Game.Score(out strikes, out spares);
                var status = Game.RollStatus;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Game {i + 1} score :{score} strikes:{strikes} spares:{spares}");
                Console.ForegroundColor = ConsoleColor.White;
                if (score > highestScore) highestScore = score;
            }
            Console.WriteLine("Highest score is : {0} ", highestScore);
        }
    }
}

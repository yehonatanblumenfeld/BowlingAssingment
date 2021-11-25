using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAssingment.Bowling
{
    public class Game
    {
        private static RollStatusEnum _rollStatus;

        public static RollStatusEnum RollStatus
        {
            get { return _rollStatus; }
            set { _rollStatus = value; }
        }

        public static int Roll(bool isLastRound)
        {
            int firstBall = 0, secondBall = 0, bonusBall = 0;
            int Throws = 0;
            int TotalPins = 10;
            var currectStatus = _rollStatus;
            int numOfRolls = 2;
            int TotalScore = 0;

            for (int i = 0; i < numOfRolls; i++)
            {
                Random r = new Random();
                if (Throws == 0)// checks if its your first throw
                {
                    
                    firstBall = r.Next(0, TotalPins + 1);
                    if (firstBall.Equals(TotalPins))// checks if u droped all pins in the first throw --> strike
                    {
                        _rollStatus = RollStatusEnum.Strike;
                        if(isLastRound is true) 
                        {
                            numOfRolls += 1;
                            i += 1;
                            Throws = 2;
                            continue;
                        };
                        TotalScore = firstBall;
                        break;
                    }
                    Throws++;
                    continue;
                }
                if (Throws == 2)// checks if its your bonus Ball
                {
                    int pinsLeft = TotalPins - (firstBall + secondBall);
                    switch (_rollStatus)
                    {
                        case RollStatusEnum.Normal:
                            bonusBall = r.Next(0, pinsLeft + 1);
                            break;
                        case RollStatusEnum.Spare:
                            bonusBall = r.Next(0, TotalPins + 1);
                            break;
                        case RollStatusEnum.Strike:
                            bonusBall = r.Next(0, TotalPins + 1);
                            break;                  
                    }                  
                    TotalScore = firstBall + secondBall + bonusBall;
                }
                else
                {
                    int pinsLeft = TotalPins - firstBall;
                    secondBall = r.Next(0, pinsLeft + 1);//number of pins dropped in the second ball
                    if (secondBall + firstBall == TotalPins)// checks if u droped all pins in second throw --> spare 
                    {
                        if (isLastRound is true) 
                        { 
                            numOfRolls += 1;
                            Throws = 2;
                        }
                        _rollStatus = RollStatusEnum.Spare;
                        TotalScore = secondBall + firstBall;
                        break;
                    }
                    if ((secondBall + firstBall) < TotalPins && isLastRound is true)
                    {
                        continue;
                    }
                    _rollStatus = RollStatusEnum.Normal;//not strike or spare
                    TotalScore = secondBall + firstBall;
                }
            }
            switch (currectStatus)
            {
                case RollStatusEnum.Normal:
                    break;
                case RollStatusEnum.Spare:
                    TotalScore += firstBall;//first ball spare bonus                   
                    break;
                case RollStatusEnum.Strike:
                    TotalScore += firstBall + secondBall;//first ball and second strike bonus
                    break;
            }
            switch (_rollStatus)
            {
                case RollStatusEnum.Normal:
                    Console.WriteLine($"first throw: {firstBall} pins");
                    Console.WriteLine($"Second throw: {secondBall} pins");
                    break;
                case RollStatusEnum.Spare:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Nice Spare!! You are pretty good!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"first throw: {firstBall} pins");
                    Console.WriteLine($"Second throw: {secondBall} pins");
                    if (isLastRound) { Console.WriteLine($"Bonus throw: {firstBall} pins"); }
                    break;
                case RollStatusEnum.Strike:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Strike!!! You are Bowling God!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"first throw: {firstBall} pins!");
                    if (isLastRound) { Console.WriteLine($"Bonus throw: {firstBall} pins"); }
                    break;
            }
            return TotalScore;
        }
        public static int Score(out int strikes, out int spares)
        {
            strikes = 0;
            spares = 0;
            int score = 0;
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Frame {0}:" ,i +1 );
                Console.ForegroundColor = ConsoleColor.White;

                if (_rollStatus == Game.RollStatusEnum.Strike) strikes += 1;
                else if (_rollStatus == Game.RollStatusEnum.Spare) spares += 1;
                if (i == 9)
                {
                    score += Roll(true);
                }
                else score += Roll(false);               
            }
            return score;
        }
        public enum RollStatusEnum
        {
            Normal,
            Spare,
            Strike
        };
    }
}

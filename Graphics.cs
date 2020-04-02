using CovidSimulation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CovidSimulation
{
    public static class Graphics
    {
        public enum Status { Dead = 0, Imune = 1, Infected = 2, NotInfected = 3}
        public static void Draw(int row, int col, Status status)
        {
            switch (status)
            {
                case Status.Dead:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Status.Imune:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Status.Infected:
                    Console.ForegroundColor = ConsoleColor.Red;                    
                    break;
                case Status.NotInfected:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            if (row == 0)
            {
                row = 1;
            }
            // Console.WriteLine($"Generating on Row {row}, Col {col}");
            //Console.SetCursorPosition(col, row - 1);
            Console.SetCursorPosition(col, row);
            Console.Write(RunTimeConfig.HumanIcon);
            Console.ResetColor();
        }

        public static void Draw(Human human)
        {
            if (human.Infected)
                Console.ForegroundColor = ConsoleColor.Red;
            if (human.Imune)
                Console.ForegroundColor = ConsoleColor.Green;
            if (!human.Infected)
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (!human.Alive)
                Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(human.Col, human.Row);
            Console.Write(RunTimeConfig.HumanIcon);
            Console.ResetColor();
        }
         
        public static void ClearPos(int row, int col)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(" ");
        }

        public static void Move(Human human, int col, int row)
        {
            if (col >= RunTimeConfig.CWidth || row >= RunTimeConfig.CHeight || !human.Alive)
            {
                return;
            }

            human.ExCol = human.Col;
            human.ExRow = human.Row;
            human.Row = row;
            human.Col = col;

            Draw(row, col, Status.NotInfected); //TODO: Change!
        }

        public static void UpdateGenerationStatistic(int humansnow, int humansmax)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write($"Generating Humans [{humansnow}/{humansmax}]");
            Console.ResetColor();
        }

        public static void BeginSimulation()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, Console.WindowHeight - 1);

            for (int i = RunTimeConfig.SimulateBreak; i >= 0; i--)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                Console.Write($"Starting Simulation in {i}s...");
                Thread.Sleep(1000);
            }
            
            Console.ResetColor();
        }
        public static void DrawStatusBar(int day, int healthy, int infected, int imune, int dead)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, Console.WindowHeight - 1);

            DateTime startDate = RunTimeConfig.DateTime;
            

            Console.Write($"[{startDate.AddDays(day).ToShortDateString()}] Healhty: {healthy} Infected: {infected - dead} Imune: {imune} Dead: {dead}  ");
            Console.ResetColor();
        }

    }
}

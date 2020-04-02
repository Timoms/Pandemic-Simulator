using System;
using System.Collections.Generic;
using System.Text;

namespace CovidSimulation
{
    public static class RunTimeConfig
    {
        public static int CWidth { get; set; }
        public static int CHeight { get; set; }
        public static int DayDurationMS { get; set; }
        public static int SimulateBreak { get; set; }
        public static DateTime DateTime { get; set; }
        public static string HumanIcon { get; set; }
        public static float InfectionProbability { get; set; }
        public static float InfectionRadius { get; set; }
        public static float PopulationDensity { get; set; }
    }

    public static class Helper
    {
        
    }

    public static class Generator
    {
        public static Random _Rand = new Random();
        public static int Next(int lower, int upper)
        {
            return _Rand.Next(lower, upper);
        }

        public static bool PercentageBool (float probabilty)
        {
            //return _Rand.NextDouble() < probabilty;
            return _Rand.Next(100) <= probabilty ? true : false;
        }
    }
}

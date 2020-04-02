using System;
using System.Text;
using CovidSimulation.Models;

namespace CovidSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTimeConfig.CHeight = 50;
            RunTimeConfig.CWidth = 60;
            RunTimeConfig.DayDurationMS = 300;
            RunTimeConfig.SimulateBreak = 0;
            RunTimeConfig.DateTime = new DateTime(2020, 1, 1);
            RunTimeConfig.HumanIcon = "■";
            RunTimeConfig.InfectionProbability = 0.10f;
            RunTimeConfig.InfectionRadius = 2.00f;
            RunTimeConfig.PopulationDensity = 0.01f;


            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(RunTimeConfig.CWidth + 2, RunTimeConfig.CHeight + 5);
            Console.SetBufferSize(RunTimeConfig.CWidth + 2, RunTimeConfig.CHeight + 5);

            

            

            Simulation simulation = new Simulation();
            Console.Title = $"Pandemic Simulation on {simulation.MaxHumans} Humans";


            simulation.RunAutonomus();
            

            Console.ReadLine();

        }
    }
}

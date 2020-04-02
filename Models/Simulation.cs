using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CovidSimulation.Models
{
    public class Simulation
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public float PopulationDesity { get; set; }
        public float InfectionRadius { get; set; }
        public float InfectionProbability { get; set; }
        public int MaxHumans { get; set; }
        public int CurrentHumans { get; set; }
        public List<Human> Humans { get; set; }
        public int Healthy { get; set; }
        public int Infected { get; set; }
        public int Imune { get; set; }
        public int Dead { get; set; }
        public int SimulationDay { get; set; }

        public Simulation()
        {
            Width = RunTimeConfig.CWidth;
            Height = RunTimeConfig.CHeight;
            PopulationDesity = RunTimeConfig.PopulationDensity;
            InfectionProbability = RunTimeConfig.InfectionProbability;
            InfectionRadius = RunTimeConfig.InfectionRadius;
            MaxHumans = GetMaxHumans();
            CurrentHumans = 0;
            Humans = new List<Human>();
        }

        public void RunAutonomus()
        {
            GenerateHumans();
            Graphics.BeginSimulation();
            SimulateDays();
        }

        private void InfektionVerbreiten()
        {
            Humans.FindAll(e => e.Infected).ForEach(e =>
            {
                Humans.ForEach(all =>
                {
                    if (Distance(all, e) <= InfectionRadius)
                    {
                        all.Infect();
                    }
                });

            });
        }

        private void SimulateDays()
        {
            while (Humans.FindAll(e => e.Alive).Count > 0)
            {
                Console.Clear();

                Humans.ForEach(e => {
                    e.SimulateDay();
                    e.Move(this);
                });

                InfektionVerbreiten();

                var healthy = Humans.FindAll(e => !e.Infected).Count;
                var infected = Humans.FindAll(e => e.Infected).Count;
                var imune = Humans.FindAll(e => e.Imune).Count;
                var dead = Humans.FindAll(e => !e.Alive).Count;

                Graphics.DrawStatusBar(SimulationDay, healthy, infected, imune, dead);
                SimulationDay++;
                DrawHumans();
                Thread.Sleep(RunTimeConfig.DayDurationMS);
            }

        }

        public void GenerateHumans()
        {
            while (CurrentHumans <= MaxHumans)
            {
                Position x = GetFreeRandomField();
                Graphics.UpdateGenerationStatistic(CurrentHumans, MaxHumans);
                Humans.Add(new Human(x.Column, x.Row));
                //Graphics.Draw(x.Row, x.Column, Graphics.Status.Dead);
                CurrentHumans++;
            }
            int index = Generator.Next(0, Humans.Count);

            Humans[index].Infect();
            DrawHumans();
        }

        private void DrawHumans()
        {
            Humans.ForEach(e =>
            {
                Graphics.Draw(e);
            });
        }

        private int GetMaxHumans()
        {
            return Convert.ToInt32(Math.Round(RunTimeConfig.CWidth * RunTimeConfig.CHeight * PopulationDesity, 0));
        }

        private double Distance(Human h1, Human h2)
        {
            return Math.Sqrt(((h1.Col - h2.Col) ^ 2) + ((h1.Row - h2.Row) ^ 2));
        }

        #region FieldGeneration
        public bool IsFieldFree(Position position)
        {
            if (Humans.Find(e => e.Col == position.Column && e.Row == position.Row) != null)
            {
                return false;
            }
            return true;
        }

        private Position FreePosition;

        private Position GetGuess()
        {
            Position po = new Position(Generator.Next(0, RunTimeConfig.CHeight), Generator.Next(0, RunTimeConfig.CWidth));
            FreePosition = po;
            return po;
        }

        private Position GetFreeRandomField()
        {
            int width = RunTimeConfig.CWidth;
            int height = RunTimeConfig.CHeight;

            while (!IsFieldFree(GetGuess())) ;

            return FreePosition;
        }


        /// <summary>
        /// Gets next free field in the console
        /// </summary>
        /// <returns>Row and Column in a Dictionary</returns>
        private Position GetFreeField()
        {
            int width = RunTimeConfig.CWidth;
            int height = RunTimeConfig.CHeight;

            for (int i = 0; i <= height; i++) // Loop through rows
            {
                for (int j = 0; j <= width; j++) // Loop through columns
                {
                    if (Humans.Find(e => e.Col == j && e.Row == i) == null)
                    {
                        return new Position(i, j);
                    }
                }
            }

            return null;
        }
        #endregion
    }
}

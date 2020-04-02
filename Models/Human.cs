using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CovidSimulation.Models
{
    public class Human
    {
        

        public int Row { get; set; }
        public int Col { get; set; }
        public int ExCol { get; set; }
        public int ExRow { get; set; }
        public int Age { get; set; }
        public bool HasLungDisease { get; set; }
        public bool Infected { get; set; }
        public int InfectedSince { get; set; }
        public bool Imune { get; set; }
        public bool InQuarantine { get; set; }
        public bool Alive { get; set; }

        public Human(int col, int row)
        {
            Row = row;
            Col = col;
            ExCol = 0;
            ExRow = 0;
            Age = Generator.Next(10, 105);
            HasLungDisease = Generator.PercentageBool(3);
            Infected = false;
            Imune = false;
            InQuarantine = false;
            Alive = true;
            InfectedSince = 0;
        }

        public void Move(Simulation sim)
        {
            if (!Alive)
                return;

            var rand = Generator.Next(0, 4);
            

            switch (rand)
            {
                case 0: //Up
                    if ((Row - 1 > 0) && sim.IsFieldFree(new Position(this.Row -1, this.Col)))
                    {
                        ExCol = Col;
                        ExRow = Row;
                        Row = ExRow - 1;
                    }
                    break;
                case 1: //Down
                    if ((Row + 1 < RunTimeConfig.CHeight) && sim.IsFieldFree(new Position(this.Row + 1, this.Col)))
                    {
                        ExCol = Col;
                        ExRow = Row;
                        Row = ExRow + 1;
                    }
                    break;
                case 2: //Left
                    if ((Col - 1 > 0) && sim.IsFieldFree(new Position(this.Row, this.Col - 1)))
                    {
                        ExCol = Col;
                        ExRow = Row;
                        Col = ExCol - 1;
                    }
                    break;
                case 3: //Right
                    if ((Col + 1 < RunTimeConfig.CWidth) && sim.IsFieldFree(new Position(this.Row, this.Col + 1)))
                    {
                        ExCol = Col;
                        ExRow = Row;
                        Col = ExCol + 1;
                    }
                    break;
                case 4:
                    break;
            }


        }

        public bool IsInfectious()
        {
            if (Alive && Infected)
            {
                return true;
            }
            return false;
        }

        public bool IsRiskPatient()
        {
            if (Age >= 70 || HasLungDisease)
            {
                return true;
            }
            return false;
        }

        public void SimulateDay()
        {
            if (Alive && Infected)
            {
                InfectedSince++;
                if (Generator.PercentageBool(1))
                {
                    Alive = false;
                    return;
                }
                if (InfectedSince > 13)
                {
                    Imune = Generator.PercentageBool(20);
                }
            }

            //Graphics.Move(this, ++Col, ++Row); //TODO: Is this the right logic?

        }

        public void Infect()
        {
            Infected = true;
        }

    }
}

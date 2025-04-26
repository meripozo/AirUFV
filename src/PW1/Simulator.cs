using System;

namespace AirportSimulation
{
    public class Simulator
    {
        private Airport airport;

        public Simulator()
        {
            airport = new Airport();
            airport.AddRunway(new Runway("Runway-1"));
            airport.AddRunway(new Runway("Runway-2"));
        }
        public void LoadAircraftFromFile(string path)
        {
            bool result = airport.LoadAircraftFromFile(path);
            if (result) Console.WriteLine("Flights loaded successfully.");
        }
        public void AddAircraft()
        {
            airport.AddAircraft();
        }

        public void RunSimulationManu()
        {

            Console.WriteLine(airport.GetNumberOfAircrafts());
            Console.WriteLine("Starting simulation. Press any key to advance one tick, or '*' to quit simulation.");
            bool simulationRunning = true;
            while (simulationRunning)
            {
                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("Current system status:");
                airport.ShowStatus();
                Console.WriteLine("\nPress any key to advance one tick, or '*' to quit simulation.");
                string input = Console.ReadLine();
                if (input == "*")
                {
                    simulationRunning = false;
                }
                else
                {
                    airport.AdvanceTick();
                }
            }
        }
    }
}
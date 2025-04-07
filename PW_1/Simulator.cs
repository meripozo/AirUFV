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

        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                
                Console.WriteLine("\n╔═══════════════════════════════════════╗");
                Console.WriteLine("║    Airport Landing Simulation Menu    ║");
                Console.WriteLine("║                                       ║");
                Console.WriteLine("║ 1. Load flights from file             ║");
                Console.WriteLine("║ 2. Add a flight manually              ║");
                Console.WriteLine("║ 3. Start simulation (Manual)          ║");
                Console.WriteLine("║ 4. Exit                               ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.Write("Select an option: ");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter file path: ");
                        string filePath = Console.ReadLine();
                        bool result = airport.LoadAircraftFromFile(filePath);
                        if (result) Console.WriteLine("Flights loaded successfully.");
                        Console.ReadLine();
                        break;
                    case 2:
                        airport.AddAircraft();
                        break;
                    case 3:
                        RunSimulationManu();
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void RunSimulationManu()
        {
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

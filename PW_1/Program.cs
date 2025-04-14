using System;
namespace AirportSimulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            public void MainMenu()
            {
                Simulator simulator = new Simulator(); //we instantiate Simulator and Airport classes
                Airport airport = new Airport();

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
                            simulator.RunSimulationManu();
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
        }
    }
}
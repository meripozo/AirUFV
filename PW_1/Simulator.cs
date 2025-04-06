using System;

namespace AirportSimulation
{
    public class Simulator
    {
        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n╔═══════════════════════════════════════╗");
                Console.WriteLine("║    Airport Landing Simulation Menu    ║");
                Console.WriteLine("║                                       ║");
                Console.WriteLine("║ 1. Load flights from file             ║");
                Console.WriteLine("║ 2. Add a flight manually              ║");
                Console.WriteLine("║ 3. Start simulation (Manual)          ║");
                Console.WriteLine("║ 4. Start simulation (Automatic)       ║");
                Console.WriteLine("║ 5. Exit                               ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.Write("Select an option: ");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                    // Add from file
                        break;
                    case 2:
                    // Add aircraft manually
                        break;
                    case 3:
                    // Simulate manually
                        break;
                    case 4:
                    // Simulate auto (extra)
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}

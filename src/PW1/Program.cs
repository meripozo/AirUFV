using System;

namespace AirportSimulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //we instantiate Simulator class
            Simulator simulator = new Simulator(); 

            // properties for controlling flow of program
            bool exit = false;

            int option = 0;
            
            while (!exit) //cambiar menú:  no hacerlo con la clase abst, msimplemente cambiar la condición dle propio bucle
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine();
                    Console.WriteLine("╔═══════════════════════════════════════╗");
                    Console.WriteLine("║    Airport Landing Simulation Menu:   ║");
                    Console.WriteLine("║                                       ║");
                    Console.WriteLine("║ 1. Load flights from file             ║");
                    Console.WriteLine("║ 2. Add a flight manually              ║");
                    Console.WriteLine("║ 3. Start simulation (Manual)          ║");
                    Console.WriteLine("║ 4. Exit                               ║");
                    Console.WriteLine("╚═══════════════════════════════════════╝");
                    Console.Write("Select an option: ");

                    option = Convert.ToInt32(Console.ReadLine());

                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, please enter the correct data type.");
                }

                switch (option)
                {
                    case 1:
                        Console.Write("Enter file path: ");
                        string filePath = Console.ReadLine();
                        // load aircraft from file using path given by user
                        simulator.LoadAircraftFromFile(filePath);
                        Console.ReadLine();
                        break;
                    case 2:
                        // we add aircraft manually
                        simulator.AddAircraft();
                        break;
                    case 3:
                         // the simulation is started manually
                        simulator.RunSimulationManu();
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please, select an option between 1 and 4.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
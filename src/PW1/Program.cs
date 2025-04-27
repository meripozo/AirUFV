using System;

namespace AirportSimulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Simulator simulator = new Simulator(); // instantiation of Simulator and Airport classes

            ValidInt validInt = new ValidInt(); // instantiation of ValidInt class for sleected option in menu

            // properties for controlling flow of program
            bool exit = false;
            bool isValid = false;

            int option = 0;
            
            while (!exit)
            {
                try
                {
                    do
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

                        isValid = validInt.validateInput(Convert.ToString(option)); // validate input using ValidInt class

                    }while(!isValid); // doesn´t stop looping until valid input is given

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
                        simulator.LoadAircraftFromFile(filePath); // load aircraft from file using path given by user
                        Console.ReadLine();
                        break;
                    case 2:
                        simulator.AddAircraft(); // add aircraft manually
                        break;
                    case 3:
                        simulator.RunSimulationManu(); // start simulation manually
                        break;
                    case 4:
                        exit = true; // exit program
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
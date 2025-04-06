using System;

namespace AirportSimulation
{
    public abstract class Airport
    {
        public List<Runway> Runways { get; set; }
        public List<Aircraft> Aircrafts {get; set;}

        public Airport()
        {
            Runways = new List<Runway>();
            Aircrafts = new List<Aircraft>();
            //add runway list
        }

        // Adds runway to runways list
        public void AddRunway(Runway runway)
        {
            Runways.Add(runway);
        }
        
        public void ShowStatus()
        {
            Console.WriteLine("Runway Status:");
            foreach (var runway in Runways)
            {
                Console.WriteLine(runway.ToString());
            }
            
            //show aircraft status 
            Console.WriteLine("\nAircraft Status:");
            foreach (var aircraft in Aircrafts)
            {
                Console.WriteLine(aircraft.ToString());
            }
        }

        // Advance simulation by one tick (15 mins)
        public void AdvanceTick()
        {
            foreach (var aircraft in Aircrafts)
            {
                if (aircraft.Status != AircraftStatus.OnGround)
                {
                    aircraft.UpdateTick();
                }
            }

            foreach (var aircraft in Aircrafts)
            {
                if (aircraft.Status == AircraftStatus.Waiting)
                {
                    foreach (var runway in Runways)
                    {
                        if (runway.Status == RunwayStatus.Free)
                        {
                            runway.RequestRunway(aircraft);
                            return;
                        }
                    }
                }
            }

            // Update runways to process landing ticks
            foreach (var runway in Runways)
            {
                runway.UpdateTick();
            }
        }

        // Load aircraft from a CSV file
        // If return true = flights loaded successfully
        // If return flase = flights not loaded successfully
        public bool LoadAircraftFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return false;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    // ID;State;Distance;Speed;Type;FuelCapacity;FuelConsumption;AdditionalData
                    string[] parts = line.Split(';');
                    if (parts.Length < 8)
                    {
                        Console.WriteLine("File format error.");
                        return false;
                    }
                    string id = parts[0];
                    if (!Enum.TryParse(parts[1], out AircraftStatus status))
                    {
                        Console.WriteLine("State parsing error.");
                        return false;
                    }

                    int distance = int.Parse(parts[2]);
                    int speed = int.Parse(parts[3]);
                    string type = parts[4];
                    double fuelCapacity = double.Parse(parts[5]);
                    double fuelConsumption = double.Parse(parts[6]);

                    double currentFuel = fuelCapacity; // maxed fuel before starting flight

                    // StringComparison for upper and lower case letter compatibility
                    if ("Commercial", StringComparison.OrdinalIgnoreCase)
                    {
                        int numPassengers = int.Parse(parts[7]);
                        Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                    }
                    else if ("Cargo", StringComparison.OrdinalIgnoreCase)
                    {
                        double maxLoad = double.Parse(parts[7]);
                        Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                    }
                    else if ("Private", StringComparison.OrdinalIgnoreCase)
                    {
                        string owner = parts[7];
                        Aircrafts.Add(new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, owner));
                    }

                    else
                    {
                        Console.WriteLine("Unknown aircraft type.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
                return false;
            }
            return true;
        }


        // we Add an aircraft manually with the users console input
        public void AddAircraft()
        {
            // Aircraft Type
            Console.WriteLine("Select an Aircraft Type:");
            Console.WriteLine(" 1. Commercia");
            Console.WriteLine(" 2. Cargo");
            Console.WriteLine(" 3. Private");

            int selectedType = Convert.ToInt32(Console.ReadLine());
            if (selectedType > 3 || selectedType < 1)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            // ID
            Console.Write("Enter Aircraft ID: ");
            string id = Console.ReadLine();

            // State
            Console.Write("Enter initial state (InFlight, Waiting, Landing, OnGround): ");
            string stateInput = Console.ReadLine();
            if (!Enum.TryParse(stateInput, out AircraftStatus status)) // PREGUNTAR A MOISÉS
            {
                Console.WriteLine("Invalid state.");
                return;
            }
            
            // Distance
            Console.Write("Enter distance from airport (km): ");
            int distance = Convert.ToInt32(Console.ReadLine());

            // Speed
            Console.Write("Enter speed (km/h): ");
            int speed = Convert.ToInt32(Console.ReadLine());

            // Fuel Capacity
            Console.Write("Enter fuel capacity (liters): ");
            double fuelCapacity = Convert.ToDouble(Console.ReadLine());

            // Fuel Consumption
            Console.Write("Enter fuel consumption (liters/km): ");
            double fuelConsumption = Convert.ToDouble(Console.ReadLine());
            double currentFuel = fuelCapacity; // maxed tank before flight

        }


        //create load 

        //need to specify the aircraft type too
    }
}

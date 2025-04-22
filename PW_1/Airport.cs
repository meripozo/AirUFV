using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace AirportSimulation
{
    public class Airport
    {
        //public List<Runway> Runways { get; set; }       //no se puede hacer esto
        //public List<Aircraft> Aircrafts { get; set; }

        private List<Runway> Runways;
        private List<Aircraft> Aircrafts;
        public Airport()
        {
            Runways = new List<Runway>();
            Aircrafts = new List<Aircraft>();
        }

        // Adds runway to runways list
        public void AddRunway(Runway runway)
        {
            Runways.Add(runway);
        }

        // Display the status of all runways and aircraft
        public void ShowStatus()
        {
            Console.WriteLine("Runway Status:");
            foreach (var runway in Runways)
            {
                Console.WriteLine(runway.ToString());
            }


            Console.WriteLine("\nAircrafts Information: ");
            Console.WriteLine($"Number of Aircrafts loaded: {Aircrafts.Count}");
            foreach (var aircraft in Aircrafts)
            {
                Console.WriteLine(aircraft.ToString());
            }
        }

        public int GetNumberOfAircrafts()
        {
            return Aircrafts.Count;
        }

        // Advance simulation by one tick (15 mins) (update runway and aircrafts)
        public void AdvanceTick()
        {
            // Update each aircraft (skip if OnGround)
            foreach (var aircraft in Aircrafts)
            {
                if (aircraft.Status != 4)
                {
                    aircraft.UpdateTick();
                }
            }

            // Attempt to assign waiting aircraft to free runways so it can land
            foreach (var aircraft in Aircrafts)
            {
                if (aircraft.Status == 2)
                {
                    bool searchingRunway = true;
                    while(searchingRunway)
                    {
                        foreach (var runway in Runways)
                        {
                            if (runway.Status == RunwayStatus.Free)
                            {
                                runway.RequestRunway(aircraft);
                                searchingRunway = false;
                                //return; // no se puede usar
                            }
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

        public bool LoadAircraftFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                Console.ReadLine();
                return false;
            }

            try
            {
                StreamReader sr = File.OpenText(filePath);  //ahora en la variable fileSr tengo todo el documento

                string line = sr.ReadLine(); // se salte la primera línea del documento al leerlo

                while ((line = sr.ReadLine()) != null) //bucle que va de línea en línea, hasta que ya no encuentra líneas. 
                {
                    // ID;State;Distance;Speed;Type;FuelCapacity;FuelConsumption;AdditionalData
                    string[] parts = line.Split(';');
                    if (parts.Length < 8)
                    {
                        Console.WriteLine("File format error.");
                        Console.ReadLine();
                        return false;
                    }

                    string id = parts[0];

                    int status = 0;
                    if(parts[1] == "InFlight")   //esto está bien!!
                    {
                        status = 1;
                    }
                    else if(parts[1] == "Waiting")
                    {
                        status = 2;
                    }
                    else if(parts[1] == "Landing")
                    {
                        status = 3;
                    }
                    else if(parts[1] == "OnGround")
                    {
                        status = 4;
                    } 

                    // int status = Convert.ToInt32(parts[1]); // no funciona ya que se le pasa un string y este coge el int que le corresponde
                    int distance = Convert.ToInt32(parts[2]);
                    int speed = Convert.ToInt32(parts[3]);
                    string type = parts[4];
                    double fuelCapacity = Convert.ToDouble(parts[5]);
                    double fuelConsumption = Convert.ToDouble(parts[6]);

                    double currentFuel = fuelCapacity; // maxed fuel before starting flight

                    // StringComparison for upper and lower case letter compatibility
                    if (type == "Commercial")
                    {
                        int numPassengers = Convert.ToInt32(parts[7]);
                        Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                    }
                    else if (type == "Cargo")
                    {
                        double maxLoad = Convert.ToDouble(parts[7]);
                        Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                    }
                    else if (type == "Private")
                    {
                        string owner = parts[7];
                        Aircrafts.Add(new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, owner));
                    }
                    else
                    {
                        Console.WriteLine("Unknown aircraft type.");
                        Console.ReadLine();
                        return false;
                    }
                }
                Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
                Console.ReadLine();
                return false;
            }
            return true;
        }

        // Adds an aircraft manually with the users console input
        public void AddAircraft()
        {
            // Aircraft Type
            Console.WriteLine("Select an Aircraft Type:");
            Console.WriteLine(" 1. Commercial");
            Console.WriteLine(" 2. Cargo");
            Console.WriteLine(" 3. Private");
            
            int selectedType = Convert.ToInt32(Console.ReadLine());
            if (selectedType > 3 || selectedType < 1)
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadLine();
                // condicion que salga de AddAircraft() usando while() x ejemplo
            }

            // ID
            Console.Write("Enter Aircraft ID: ");
            string id = Console.ReadLine();

            // State
            Console.Write("Enter initial state, enter number: (1. InFlight, 2. Waiting, 3. Landing, 4. OnGround): ");
            int status = Convert.ToInt32(Console.ReadLine());

            // Distance & Speed
            int distance = 0;
            int speed = 0;
            if (status == 1)
            {
                Console.Write("Enter distance from airport (km): ");
                distance = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter speed (km/h): ");
                speed = Convert.ToInt32(Console.ReadLine());
            }
            
            // Fuel Capacity
            Console.Write("Enter fuel capacity (liters): ");
            double fuelCapacity = Convert.ToDouble(Console.ReadLine());

            // Fuel Consumption
            double fuelConsumption = 0;
            if(status == 1 || status == 2 || status == 3)
            {
                Console.Write("Enter fuel consumption (liters/km): ");
                fuelConsumption = Convert.ToDouble(Console.ReadLine());
            }
            
            double currentFuel = fuelCapacity; // maxed tank before flight

            // Additional Data
            switch(selectedType)
            {
                case 1:
                    Console.Write("Enter number of passengers: ");
                    int numPassengers = Convert.ToInt32(Console.ReadLine());
                    Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                    Console.WriteLine("Commercial Aircraft Successfully added!");
                    Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                    Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Enter maximum load (kg): ");
                    double maxLoad = Convert.ToDouble(Console.ReadLine());
                    Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                    Console.WriteLine("Cargo Aircraft Successfully added!");
                    Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Enter owner name: ");
                    string owner = Console.ReadLine();
                    Aircrafts.Add(new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, owner));
                    Console.WriteLine("Private Aircraft Successfully added!");
                    Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Invalid aircraft type selection.");
                    Console.ReadLine();
                    break;
            }
        }

        //error checking 
        public bool CheckString()
        {
            try
            {
                
            }
            catch(FormatException e)
            {
                Console.WriteLine("");
                Console.WriteLine("Error, please enter the correct data type");
                Console.ReadKey();
            }
            return true;
        }
        public bool CheckInt()
        {
            try
            {

            }
            catch(FormatException e)
            {
                Console.WriteLine("");
                Console.WriteLine("Error, please enter the correct data type");
                Console.ReadKey();
            }
            return true;
        }
        public bool CheckDouble()
        {
            try
            {
                
            }
            catch(FormatException e)
            {
                Console.WriteLine("");
                Console.WriteLine("Error, please enter the correct data type");
                Console.ReadKey();
            }
            return true;
        }
    }
}
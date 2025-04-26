using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace AirportSimulation
{
    public class Airport
    {
        private List<Runway> Runways;
        private List<Aircraft> Aircrafts;
        private int selectedType = 0;
        private string id = "";
        private int status = 0;
        private int distance = 0;
        private int speed = 0;
        private double fuelCapacity = 0;
        private double fuelConsumption = 0;
        private double currentFuel = 0;
        private string owner = "";
        private int numPassengers = 0;
        private double maxLoad = 0;

        private bool isValid = false; // for testing purposes

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
                runway.PrintRunwayInfo();
            }


            Console.WriteLine("\nAircrafts Information: ");
            Console.WriteLine($"Number of Aircrafts loaded: {Aircrafts.Count}");
            foreach (var aircraft in Aircrafts)
            {
                aircraft.PrintAircraftInfo();
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
            try
            {
                if (File.Exists(filePath))
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

                        // ID
                        id = parts[0];

                        // State
                        if(parts[1] == "InFlight")
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

                        distance = Convert.ToInt32(parts[2]);
                        speed = Convert.ToInt32(parts[3]);
                        fuelCapacity = Convert.ToDouble(parts[5]);
                        fuelConsumption = Convert.ToDouble(parts[6]);

                        currentFuel = fuelCapacity; // maxed fuel before starting flight

                        // StringComparison for upper and lower case letter compatibility
                        string type = parts[4].ToLower();
                        if (type == "commercial")
                        {
                            int numPassengers = Convert.ToInt32(parts[7]);
                            Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                        }
                        else if (type == "cargo")
                        {
                            double maxLoad = Convert.ToDouble(parts[7]);
                            Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                        }
                        else if (type == "private")
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
                else
                {
                    Console.WriteLine("File not found.");
                    return false;
                }
            }
            catch (Exception ex)  //cambiar tipo de exception
            {
                Console.WriteLine("Error loading file: " + ex.Message);
                return false;
            }
            return true;    //we return true to "if (result) Console.WriteLine("Flights loaded successfully.");" in simulator, when the try is executed
        }

        // Adds an aircraft manually with the users console input
        public void AddAircraft()
        {   
            // Aircrfat Type
            try
            {
                do
                {
                    Console.WriteLine("Select an Aircraft Type:");
                    Console.WriteLine(" 1. Commercial");
                    Console.WriteLine(" 2. Cargo");
                    Console.WriteLine(" 3. Private");
                    selectedType = Convert.ToInt32(Console.ReadLine());
                    isValid = Verifications.IsValidInt(selectedType); // check if selectedType is valid

                    if (selectedType > 3 || selectedType < 1)
                    {
                        Console.WriteLine("Invalid selection.");
                    }

                } while (false || selectedType > 3 || selectedType < 1); //validation
                isValid = false; // reset for next input
            }
            catch (FormatException)
            {
                Console.WriteLine("Error, please enter the correct data type");
            }

            // ID
            try
            {
                do
                {
                    Console.Write("Enter Aircraft ID: ");
                    id = Console.ReadLine();
                    isValid = Verifications.IsValidString(id); // check if id is valid

                } while (false); //validation
            }
            catch (FormatException)
            {
                Console.WriteLine("Error, please enter the correct data type");
            }
            isValid = false; // reset for next input

            // State
            try
            {
                do
                {
                    Console.Write("Enter initial state, enter number: (1. InFlight, 2. Waiting, 3. Landing, 4. OnGround): ");
                    status = Convert.ToInt32(Console.ReadLine());
                    isValid = Verifications.IsValidInt(status); // check if status is valid

                    if (status < 1 || status > 4)
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }
                while (false || status < 1 || status > 4); //validation
            }
            catch (FormatException)
            {
                Console.WriteLine("Error, please enter the correct data type");
            }
            isValid = false; // reset for next input

            // Distance & Speed
            if (status == 1)
            {
                try
                {
                    do
                    {
                        Console.Write("Enter distance from airport (km): ");
                        distance = Convert.ToInt32(Console.ReadLine());
                        isValid = Verifications.IsValidDouble(distance); // check if distance is valid

                        if (distance < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type");
                        }

                    } while (false || distance < 0); //validation
                    isValid = false; // reset for next input
                    
                    do
                    {
                        Console.Write("Enter speed (km/h): ");
                        speed = Convert.ToInt32(Console.ReadLine());
                        isValid = Verifications.IsValidDouble(speed); // check if speed is valid

                        if (speed < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type");
                        }

                    } while (false || speed < 0); //validation
                    isValid = false; // reset for next input
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, please enter the correct data type");
                }
            }
            else
            {
                distance = 0;
                speed = 0;
            }
            
            // Fuel Capacity
            try
            {
                do
                {
                    Console.Write("Enter fuel capacity (liters): ");
                    fuelCapacity = Convert.ToDouble(Console.ReadLine());
                    isValid = Verifications.IsValidDouble(fuelCapacity); // check if fuelCapacity is valid

                    if (fuelCapacity < 0)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }

                } while (false || fuelCapacity < 0); //validation
            }
            catch (FormatException)
            {
                Console.WriteLine("Error, please enter the correct data type");
            }
            isValid = false; // reset for next input

            // Fuel Consumption
            if (status == 1 || status == 2 || status == 3)
            {
                try
                {
                    do
                    {
                        Console.Write("Enter fuel consumption (liters/km): ");
                        fuelConsumption = Convert.ToDouble(Console.ReadLine());
                        if (fuelConsumption < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type");
                        }
                    } while (fuelConsumption < 0); //validation
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, please enter the correct data type");
                }
            }
            else
            {
                fuelConsumption = 0;
            }
            
            currentFuel = fuelCapacity; // maxed tank before flight

            // Additional Data
            switch(selectedType)
            {
                case 1:
                    try
                    {
                        Console.Write("Enter number of passengers: ");
                        numPassengers = Convert.ToInt32(Console.ReadLine());

                        Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                        Console.WriteLine("Commercial Aircraft Successfully added!");
                        Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                        Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }
                    break;
                case 2:
                    try
                    {
                        Console.Write("Enter maximum load (kg): ");
                        maxLoad = Convert.ToDouble(Console.ReadLine());
                        
                        Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                        Console.WriteLine("Cargo Aircraft Successfully added!");
                        Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                        Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }
                    break;
                case 3:
                    try
                    {
                        Console.Write("Enter owner name: ");
                        owner = Console.ReadLine();
                        
                        Aircrafts.Add(new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, owner));
                        Console.WriteLine("Private Aircraft Successfully added!");
                        Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                        Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid aircraft type selection.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
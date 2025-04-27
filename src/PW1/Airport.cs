using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace AirportSimulation
{
    public class Airport
    {
        private Runway[] Runways;
        private List<Aircraft> Aircrafts;
        

        private bool isValid = false; // for testing purposes

        public Airport()
        {
            this.Runways = 
            [
                new Runway("Runway 1", Runway.RunwayStatus.Free, null, 3),
                new Runway("Runway 2", Runway.RunwayStatus.Free, null, 3),
                new Runway("Runway 3", Runway.RunwayStatus.Free, null, 3),
            ];
            this.Aircrafts = new List<Aircraft>();
        }


        // Display the status of all runways and aircraft
        public void ShowStatus()
        {
            Console.WriteLine("Runway Status:");
            foreach (Runway runway in Runways)
            {
                runway.PrintRunwayInfo();
            }


            Console.WriteLine("\nAircrafts Information: ");
            Console.WriteLine($"Number of Aircrafts loaded: {Aircrafts.Count}");
            foreach (Aircraft aircraft in Aircrafts)
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
            foreach (Aircraft aircraft in Aircrafts)
            {
                if (aircraft.GetStatus() != Aircraft.AircraftStatus.OnGround)
                {
                    aircraft.UpdateTick();
                }
            }

            // Attempt to assign waiting aircraft to free runways so it can land
            foreach (Aircraft aircraft in Aircrafts)
            {
                if (aircraft.GetStatus() == Aircraft.AircraftStatus.Waiting)
                {
                    bool searchingRunway = true;
                    while(searchingRunway)
                    {
                        foreach (Runway runway in Runways)
                        {
                            if (runway.GetRunwayStatus() == Runway.RunwayStatus.Free)
                            {
                                runway.RequestRunway(aircraft);
                                searchingRunway = false;
                            }
                        } 
                    }
                }
            }

            // Update runways to process landing ticks
            foreach (Runway runway in Runways)
            {
                runway.UpdateTick();
            }
        }

        public bool LoadAircraftFromFile(string filePath)
        {
            string type = "";
            string id = "";
            int speed = 0;
            int distance = 0;
            double currentFuel = 0;
            double fuelCapacity = 0;
            double fuelConsumption = 0;
            string owner = "";
            double maxLoad = 0;
            int numPassengers = 0;
            Aircraft.AircraftStatus status = 0;
            try
            {
                if (File.Exists(filePath))
                {
                    StreamReader sr = File.OpenText(filePath);  //ahora en la variable fileSr tengo todo el documento

                    string? line = sr.ReadLine(); // se salte la primera línea del documento al leerlo

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
                        try
                        {
                            id = parts[0];
                        }
                        catch
                        {
                            Console.WriteLine("Invalid ID format.");
                            return false;
                        }
                        

                        // State
                        try
                        {
                            string state = parts[1];
                        }
                        catch
                        {
                            Console.WriteLine("Invalid status format.");
                            return false;
                        }
                        
                        
                        if(parts[1] == "InFlight")
                        {
                            status = Aircraft.AircraftStatus.InFlight;
                        }
                        else if(parts[1] == "Waiting")
                        {
                            status = Aircraft.AircraftStatus.Waiting;
                        }
                        else if(parts[1] == "Landing")
                        {
                            status = Aircraft.AircraftStatus.Landing;
                        }
                        else if(parts[1] == "OnGround")
                        {
                            status = Aircraft.AircraftStatus.OnGround;
                        } 

                        // Distance
                        try
                        {
                            distance = Convert.ToInt32(parts[2]);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid distance format.");
                            return false;
                        }

                        // Speed
                        try
                        {
                            speed = Convert.ToInt32(parts[3]);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid speed format.");
                            return false;
                        }

                        // Fuel Capacity
                        try
                        {
                            fuelCapacity = Convert.ToDouble(parts[5]);
                            currentFuel = fuelCapacity; // maxed fuel before starting flight
                        }
                        catch
                        {
                            Console.WriteLine("Invalid fuel capacity format.");
                            return false;
                        }
                        
                        // Fuel Consumption
                        try
                        {
                            fuelConsumption = Convert.ToDouble(parts[6]);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid fuel consumption format.");
                            return false;
                        }


                        try
                        {
                            // StringComparison for upper and lower case letter compatibility
                            type = parts[4].ToLower();
                        }
                        catch
                        {
                            Console.WriteLine("Invalid aircraft type format.");
                            return false;
                        }
                        

                        if (type == "commercial")
                        {
                            try
                            {
                                numPassengers = Convert.ToInt32(parts[7]);
                            }
                            catch
                            {
                                Console.WriteLine("Invalid number of passengers format.");
                                return false;
                            }

                            Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                        }
                        else if (type == "cargo")
                        {
                            try
                            {
                                maxLoad = Convert.ToDouble(parts[7]);
                            }
                            catch
                            {
                                Console.WriteLine("Invalid maximum load format.");
                                return false;
                            }
                            Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                        }
                        else if (type == "private")
                        {
                            try
                            {
                                owner = parts[7];
                            }
                            catch
                            {
                                 Console.WriteLine("Invalid owner format.");
                                return false;
                            }
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
            int selectedType = 0;
            string id = "";
            int speed = 0;
            int distance = 0;
            double currentFuel = 0;
            double fuelCapacity = 0;
            double fuelConsumption = 0;
            string owner = "";
            double maxLoad = 0;
            int numPassengers = 0;
            Aircraft.AircraftStatus status = 0;

            // Aircraft Type 
            do
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Select an Aircraft Type:");
                    Console.WriteLine(" 1. Commercial");
                    Console.WriteLine(" 2. Cargo");
                    Console.WriteLine(" 3. Private");
                    selectedType = Convert.ToInt32(Console.ReadLine());

                    if (selectedType > 3 || selectedType < 1)
                    {
                        Console.WriteLine("Invalid selection.");
                        Console.ReadLine();
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Error, please enter the correct data type, not null.");
                    Console.ReadLine();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, please enter the correct data type.");
                    Console.ReadLine();
                }
                
            } while (selectedType > 3 || selectedType < 1); //validation
        
            
            // ID
            do
            {
                Console.Clear();
                try
                {
                    Console.Write("Enter Aircraft ID: ");
                    id = Console.ReadLine();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Error, please enter the correct data type, not null.");
                    Console.ReadLine();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, please enter the correct data type.");
                    Console.ReadLine();
                }
            } while (id == null || id == ""); //validation
            

            // State
            int statusAux = 0;
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write("Enter initial state, enter number: (1. InFlight, 2. Waiting, 3. Landing, 4. OnGround): ");
                    statusAux = Convert.ToInt32(Console.ReadLine());

                    switch (statusAux)
                    {
                        case 1:
                            status = Aircraft.AircraftStatus.InFlight;
                            break;
                        case 2:
                            status = Aircraft.AircraftStatus.Waiting;
                            break;
                        case 3:
                            status = Aircraft.AircraftStatus.Landing;
                            break;
                        case 4:
                            status = Aircraft.AircraftStatus.OnGround;
                            break;
                        default:
                            Console.WriteLine("Please, enter a number between 1 and 4.");
                            break;
                    }

                    if (statusAux < 1 || statusAux > 4)
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, please enter the correct data type.");
                } 
            }
            while (statusAux < 1 || statusAux > 4); //validation
            
            // Distance & Speed
            if (status == Aircraft.AircraftStatus.InFlight)
            {
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.Write("Enter distance from airport (km): ");
                        distance = Convert.ToInt32(Console.ReadLine());

                        if (distance < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error, please enter the correct data type.");
                    }
                    
                } while (distance < 0); //validation
                    
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.Write("Enter speed (km/h): ");
                        speed = Convert.ToInt32(Console.ReadLine());

                        if (speed < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error, please enter the correct data type.");
                    }
                } while (speed < 0); //validation
            }
            else
            {
                distance = 0;
                speed = 0;
            }
            
            // Fuel Capacity
        
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write("Enter fuel capacity (liters): ");
                    fuelCapacity = Convert.ToDouble(Console.ReadLine());

                    if (fuelCapacity < 0)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, please enter the correct data type");
                }
            } while (fuelCapacity < 0); //validation
             

            // Fuel Consumption
            if (status == Aircraft.AircraftStatus.InFlight || 
                status ==Aircraft.AircraftStatus.Waiting   || 
                status == Aircraft.AircraftStatus.Landing)
            {
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.Write("Enter fuel consumption (liters/km): ");
                        fuelConsumption = Convert.ToDouble(Console.ReadLine());

                        if (fuelConsumption < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }
                } while (fuelConsumption < 0); //validation
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
                    do
                    {
                        try
                        {
                            Console.Write("Enter number of passengers: ");
                            numPassengers = Convert.ToInt32(Console.ReadLine()); // verification

                            Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                            Console.WriteLine("Commercial Aircraft Successfully added!");
                            Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                            Console.ReadLine();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error, please enter the correct data type.");
                        }
                    }while(numPassengers < 0);
                    break;
                case 2:
                        do
                        {
                            try
                            {
                                Console.Write("Enter maximum load (kg): ");
                                maxLoad = Convert.ToDouble(Console.ReadLine()); // verification
                                
                                Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                                Console.WriteLine("Cargo Aircraft Successfully added!");
                                Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                                Console.ReadLine();
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Error, please enter the correct data type");
                            }
                        }while(maxLoad < 0);
                    break;
                case 3:
                    do
                    {
                        try
                        {
                            Console.Write("Enter owner name: ");
                            owner = Console.ReadLine(); // verification
                            
                            Aircrafts.Add(new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, owner));
                            Console.WriteLine("Private Aircraft Successfully added!");
                            Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                            Console.ReadLine();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error, please enter the correct data type");
                        }
                    }while(owner != null);
                    
                    break;
                default:
                    Console.WriteLine("Invalid aircraft type selection.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
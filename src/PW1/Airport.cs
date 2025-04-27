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
        

        private bool isValid = false; // for testing purposes

        public Airport()
        {
            this.Runways = new List<Runway>();
            this.Aircrafts = new List<Aircraft>();
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
        ValidInt validInt = new ValidInt();
        ValidDouble validDouble = new ValidDouble();
        ValidString validString = new ValidString();

        public bool LoadAircraftFromFile(string filePath)
        {
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
                        string id = parts[0];
                        
                        bool isValid = validString.validateInput(Convert.ToString(id)); // check if id is valid
                        if (!isValid)
                        {
                            Console.WriteLine("Invalid ID format.");
                            return false;
                        }

                        // State
                        string state = parts[1];
                        isValid = validString.validateInput(Convert.ToString(parts[1])); // check if status is valid
                        if (!isValid)
                        {
                            Console.WriteLine("Invalid status format.");
                            return false;
                        }
                        
                        Aircraft.AircraftStatus status = 0;
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
                        int distance = Convert.ToInt32(parts[2]);
                        isValid = validInt.validateInput(Convert.ToString(distance)); // check if distance is valid
                        if (!isValid)
                        {
                            Console.WriteLine("Invalid distance format.");
                            return false;
                        }

                        // Speed
                        int speed = Convert.ToInt32(parts[3]);
                        isValid = validInt.validateInput(Convert.ToString(speed)); // check if speed is valid
                        if (!isValid)
                        {
                            Console.WriteLine("Invalid speed format.");
                            return false;
                        }

                        // Fuel Capacity
                        double fuelCapacity = Convert.ToDouble(parts[5]);
                        isValid = validDouble.validateInput(Convert.ToString(fuelCapacity)); // check if fuelCapacity is valid
                        if (!isValid)
                        {
                            Console.WriteLine("Invalid fuel capacity format.");
                            return false;
                        }

                        // Fuel Consumption
                        double fuelConsumption = Convert.ToDouble(parts[6]);
                        isValid = validDouble.validateInput(Convert.ToString(fuelConsumption)); // check if fuelConsumption is valid
                        if (!isValid)
                        {
                            Console.WriteLine("Invalid fuel consumption format.");
                            return false;
                        }

                        double currentFuel = fuelCapacity; // maxed fuel before starting flight

                        // StringComparison for upper and lower case letter compatibility
                        string type = parts[4].ToLower();
                        isValid = validString.validateInput(Convert.ToString(type)); // check if type is valid
                        if (!isValid)
                        {
                            Console.WriteLine("Invalid aircraft type format.");
                            return false;
                        }

                        if (type == "commercial")
                        {
                            int numPassengers = Convert.ToInt32(parts[7]);
                            isValid = validInt.validateInput(Convert.ToString(numPassengers)); // check if numPassengers is valid
                            if (!isValid)
                            {
                                Console.WriteLine("Invalid number of passengers format.");
                                return false;
                            }

                            Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                        }
                        else if (type == "cargo")
                        {
                            double maxLoad = Convert.ToDouble(parts[7]);
                            isValid = validDouble.validateInput(Convert.ToString(maxLoad)); // check if maxLoad is valid
                            if (!isValid)
                            {
                                Console.WriteLine("Invalid maximum load format.");
                                return false;
                            }

                            Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                        }
                        else if (type == "private")
                        {
                            string owner = parts[7];
                            isValid = validString.validateInput(Convert.ToString(owner)); // check if owner is valid
                            if (!isValid)
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
                    isValid = validInt.validateInput(Convert.ToString(selectedType)); // check if selectedType is valid

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
                    Console.Clear();
                    Console.Write("Enter Aircraft ID: ");
                    id = Console.ReadLine();
                    isValid = validString.validateInput(Convert.ToString(id)); // check if id is valid

                } while (!isValid); //validation
            }
            catch (FormatException)
            {
                Console.WriteLine("Error, please enter the correct data type.");
            }

            // State
            try
            {
                int statusAux = 0;
                do
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
                    isValid = validInt.validateInput(Convert.ToString(statusAux)); // check if status is valid

                    if (statusAux < 1 || statusAux > 4)
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }
                while (!isValid || statusAux < 1 || statusAux > 4); //validation
            }
            catch (FormatException)
            {
                Console.WriteLine("Error, please enter the correct data type.");
            }

            // Distance & Speed
            if (status == Aircraft.AircraftStatus.InFlight)
            {
                try
                {
                    do
                    {
                        Console.Clear();
                        Console.Write("Enter distance from airport (km): ");
                        distance = Convert.ToInt32(Console.ReadLine());
                        isValid = validInt.validateInput(Convert.ToString(distance)); // check if distance is valid

                        if (distance < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type.");
                        }

                    } while (!isValid || distance < 0); //validation
                    
                    do
                    {
                        Console.Clear();
                        Console.Write("Enter speed (km/h): ");
                        speed = Convert.ToInt32(Console.ReadLine());
                        isValid = validInt.validateInput(Convert.ToString(speed)); // check if speed is valid

                        if (speed < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type.");
                        }

                    } while (!isValid || speed < 0); //validation
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, please enter the correct data type.");
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
                    Console.Clear();
                    Console.Write("Enter fuel capacity (liters): ");
                    fuelCapacity = Convert.ToDouble(Console.ReadLine());
                    isValid = validDouble.validateInput(Convert.ToString(fuelCapacity)); // check if fuelCapacity is valid

                    if (fuelCapacity < 0)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }

                } while (!isValid || fuelCapacity < 0); //validation
            }
            catch (FormatException)
            {
                Console.WriteLine("Error, please enter the correct data type");
            }

            // Fuel Consumption
            if (status == Aircraft.AircraftStatus.InFlight || 
                status ==Aircraft.AircraftStatus.Waiting   || 
                status == Aircraft.AircraftStatus.Landing)
            {
                try
                {
                    do
                    {
                        Console.Clear();
                        Console.Write("Enter fuel consumption (liters/km): ");
                        fuelConsumption = Convert.ToDouble(Console.ReadLine());
                        isValid = validDouble.validateInput(Convert.ToString(fuelConsumption)); // check if fuelConsumption is valid

                        if (fuelConsumption < 0)
                        {
                            Console.WriteLine("Error, please enter the correct data type");
                        }

                    } while (!isValid || fuelConsumption < 0); //validation
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
                        do
                        {
                            Console.Write("Enter number of passengers: ");
                            numPassengers = Convert.ToInt32(Console.ReadLine()); // verification

                            isValid = validInt.validateInput(Convert.ToString(numPassengers));
                            Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                            Console.WriteLine("Commercial Aircraft Successfully added!");
                            Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                            Console.ReadLine();

                        }while(!isValid);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }
                    break;
                case 2:
                    try
                    {
                        do
                        {
                            Console.Write("Enter maximum load (kg): ");
                            maxLoad = Convert.ToDouble(Console.ReadLine()); // verification
                            
                            Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                            Console.WriteLine("Cargo Aircraft Successfully added!");
                            Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                            Console.ReadLine();

                        }while(!isValid);
                        
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error, please enter the correct data type");
                    }
                    break;
                case 3:
                    try
                    {
                        do
                        {
                            Console.Write("Enter owner name: ");
                            owner = Console.ReadLine(); // verification
                            
                            Aircrafts.Add(new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, owner));
                            Console.WriteLine("Private Aircraft Successfully added!");
                            Console.WriteLine($"Loaded Aircrafts: {Aircrafts.Count}"); //temporary for testing
                            Console.ReadLine();

                        }while(!isValid);
                        
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
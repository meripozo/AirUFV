using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace AirportSimulation
{
    public class Airport
    {
        public List<Runway> Runways { get; set; }
        public List<Aircraft> Aircrafts { get; set; }

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

            Console.WriteLine("\nAircraft Status:");
            foreach (var aircraft in Aircrafts)
            {
                Console.WriteLine(aircraft.ToString());
            }
        }

        // Advance simulation by one tick (15 mins)
        public void AdvanceTick()
        {
            // Update each aircraft (skip if OnGround)
            foreach (var aircraft in Aircrafts)
            {
                if (aircraft.Status != AircraftStatus.OnGround)
                {
                    aircraft.UpdateTick();
                }
            }

            // Attempt to assign waiting aircraft to free runways so it can land
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
        // public bool LoadAircraftFromFile(string filePath)
        // {
        //     if (!File.Exists(filePath))
        //     {
        //         Console.WriteLine("File does not exist.");
        //         return false;
        //     }

        //     try
        //     {
        //         StreamReader fileSr = File.OpenText(filePath);  //ahora en la variable fileSr tengo todo el documento

        //         string line = fileSr.ReadLine(); //esto es para que se salte la primero línea del documento cuando lo lea


        //         while ((line = fileSr.ReadLine()) != null) //bucle que va de línea en línea, hasta que ya no encuentra líneas. 
        //         {
        //             //Reemplazar puntos por comas en la línea

        //             string[] lineReading = line.Split(';'); //cuando lee el ; divide el contenido de la línea y lo guarda en el array
        //             //esta función divide el contenido de line en parámetros
        //             //(se me crea una array con distintos strings de los productos que hay en cada línea ) 
        //             string[] list = line.Split(";");

        //             if (list[4] == "Commercial")
        //             {
        //                 //Console.WriteLine(double.Parse(list[3]));

        //                 Aircrafts.Add(new CommercialAircraft(     //así añadimos a la list aircrafts un aircrafts de cada tipo, con sus correspondientes atributos
        //                     list[0],
        //                     (AircraftStatus)Enum.Parse(typeof(AircraftStatus), list[1]),
        //                     int.Parse(list[2]),
        //                     int.Parse(list[3]),
        //                     double.Parse(list[5]),
        //                     double.Parse(list[4]),
        //                     double.Parse(list[6]),
        //                     int.Parse(list[7])));
        //                 Console.Clear();
        //             }
        //             else if (list[4] == "Cargo")
        //             {
        //                 Aircrafts.Add(new CargoAircraft(
        //                     list[0],
        //                     (AircraftStatus)Enum.Parse(typeof(AircraftStatus), list[1]),
        //                     int.Parse(list[2]),
        //                     int.Parse(list[3]),
        //                     int.Parse(list[4]),
        //                     int.Parse(list[5]),
        //                     int.Parse(list[6]),
        //                     int.Parse(list[7])));
        //                 Console.Clear();
        //             }
        //             else if (list[4] == "Private")
        //             {
        //                 Aircrafts.Add(new PrivateAircraft(
        //                     list[0],
        //                     (AircraftStatus)Enum.Parse(typeof(AircraftStatus), list[1]),
        //                     int.Parse(list[2]),
        //                     int.Parse(list[3]),
        //                     int.Parse(list[4]),
        //                     int.Parse(list[5]),
        //                     int.Parse(list[6]),
        //                     list[7]));
        //                 Console.Clear();
        //             }

        //             else { Console.WriteLine("There is a problem with the file or with the information"); Console.ReadKey();  }
        //         }
        //         return false;
        //     } 
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine("Error loading file: " + ex.Message);
        //         Console.ReadLine();
        //         return false;
        //     }
        // }

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
                    if (!Enum.TryParse(parts[1], out AircraftStatus status))
                    {
                        Console.WriteLine("State parsing error.");
                        Console.ReadLine();
                        return false;
                    }
                    int distance = int.Parse(parts[2]);
                    int speed = int.Parse(parts[3]);
                    string type = parts[4];
                    double fuelCapacity = double.Parse(parts[5]);
                    double fuelConsumption = double.Parse(parts[6]);

                    double currentFuel = fuelCapacity; // maxed fuel before starting flight

                    // StringComparison for upper and lower case letter compatibility
                    if (type.Equals("Commercial", StringComparison.OrdinalIgnoreCase))
                    {
                        int numPassengers = int.Parse(parts[7]);
                        Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                    }
                    else if (type.Equals("Cargo", StringComparison.OrdinalIgnoreCase))
                    {
                        double maxLoad = double.Parse(parts[7]);
                        Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                    }
                    else if (type.Equals("Private", StringComparison.OrdinalIgnoreCase))
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
                Console.ReadLine();
                return;
            }

            int distance = 0;
            if (stateInput == "InFlight")
            {
                // Distance
                Console.Write("Enter distance from airport (km): ");
                distance = Convert.ToInt32(Console.ReadLine());
            }
            
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

            // Additional Data
            switch(selectedType)
            {
                case 1:
                    Console.Write("Enter number of passengers: ");
                    int numPassengers = Convert.ToInt32(Console.ReadLine());
                    Aircrafts.Add(new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, numPassengers));
                    Console.WriteLine("Commercial Aircrfat Successfully added!");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Enter maximum load (kg): ");
                    double maxLoad = Convert.ToDouble(Console.ReadLine());
                    Aircrafts.Add(new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, maxLoad));
                    Console.WriteLine("Cargo Aircrfat Successfully added!");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Enter owner name: ");
                    string owner = Console.ReadLine();
                    Aircrafts.Add(new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel, owner));
                    Console.WriteLine("Private Aircrfat Successfully added!");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Invalid aircraft type selection.");
                    break;
            }
        }
    }
}

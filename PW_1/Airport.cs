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
                    //
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


            //aircraft atributes are still missing 
        }




        //create load 

        //need to specify the aircraft type too
    }
}

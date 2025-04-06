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
                if (aircraft.Status == AircraftStatus.OnGround)
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

        //create class load from file

        //create load 
    }
}

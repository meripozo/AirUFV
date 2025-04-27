using System;

namespace AirportSimulation
{
    public class Runway
    {
        public string id { get; set; }
        public RunwayStatus status { get; set; }
        public Aircraft currentAircraft { get; set; }
        public int ticksRemaining { get; set; } // ticks until the runway is free


        // no type assigned as considered unecessary for runway status
        public enum RunwayStatus
        {
            Free,
            Occupied
        }
        
        public const int DefaultTicksAvailability = 3; // ticks it takes to land and free runway

        public Runway(string id)
        {
            this.id = id;
            this.status = RunwayStatus.Free;
            this.currentAircraft = null;
            this.ticksRemaining = 0;
        }

        //Getter for status:
        public RunwayStatus GetRunwayStatus()
        {
            return this.status;
        }
        // Attempts to assign an aircraft to the runway for landing
        public bool RequestRunway(Aircraft aircraft)
        {
            if (status == RunwayStatus.Free && aircraft.GetStatus() == Aircraft.AircraftStatus.Waiting)
            {
                currentAircraft = aircraft;
                status = RunwayStatus.Occupied;
                ticksRemaining = DefaultTicksAvailability;
                aircraft.SetStatus(Aircraft.AircraftStatus.Landing);
                return true;
            }
            return false;
        }

        // Update the runway's occupation time for one tick.
        public void UpdateTick()
        {
            if (status == RunwayStatus.Occupied)
            {
                if (ticksRemaining > 0)
                {
                    ticksRemaining--;
                }
                if (ticksRemaining == 0 && currentAircraft != null)
                {
                    // Landing complete and aircraft is OnGround
                    currentAircraft.SetStatus(Aircraft.AircraftStatus.OnGround);
                    ReleaseRunway();
                }
            }
        }

        // Frees the runway
        public void ReleaseRunway()
        {
            currentAircraft = null;
            status = RunwayStatus.Free;
            ticksRemaining = 0;
        }

        public void PrintRunwayInfo()
        {
            if (status == RunwayStatus.Free)
            {
                Console.WriteLine($"{id}: Free");
            }
            else
            {
                Console.WriteLine($"{id}: Occupied by {currentAircraft.GetId()}, Ticks Remaining: {ticksRemaining}");
            }            
        }
    }
}
using System;

namespace AirportSimulation
{
    public class Runway
    {
        public string ID { get; set; }
        public RunwayStatus Status { get; set; }
        public Aircraft CurrentAircraft { get; set; }
        public int TicksRemaining { get; set; } // ticks until the runway is free

        public const int DefaultTicksAvailability = 3; // ticks it takes to land and free a runway

        public Runway(string id)
        {
            ID = id;
            Status = RunwayStatus.Free;
            CurrentAircraft = null;
            TicksRemaining = 0;
        }

        // Attempts to assign an aircraft to the runway for landing
        public bool RequestRunway(Aircraft aircraft)
        {
            // if runway is free and aircraft is in waiting status, assign it to the runway, runway set to occupied and strat ticks countdown
            if (Status == RunwayStatus.Free && aircraft.Status == 2)
            {
                CurrentAircraft = aircraft;
                Status = RunwayStatus.Occupied;
                TicksRemaining = DefaultTicksAvailability;
                aircraft.Status = 3;
                return true;
            }
            return false;
        }

        // Update runway's occupation time for one tick
        public void UpdateTick()
        {
            // if runway is occupied, decrease ticks remaining or free it if ticks are 0
            if (Status == RunwayStatus.Occupied)
            {
                if (TicksRemaining > 0)
                {
                    TicksRemaining--;
                }
                if (TicksRemaining == 0 && CurrentAircraft != null)
                {
                    // Landing complete and aircraft is OnGround
                    CurrentAircraft.Status = 4;
                    ReleaseRunway();
                }
            }
        }

        // frees runway when aircraft lands
        public void ReleaseRunway()
        {
            CurrentAircraft = null;
            Status = RunwayStatus.Free;
            TicksRemaining = 0;
        }

        // prints runway info on screen depending on its status
        public void PrintRunwayInfo()
        {
            if (Status == RunwayStatus.Free)
            {
                Console.WriteLine($"{ID}: Free");
            }
            else
            {
                Console.WriteLine($"{ID}: Occupied by {CurrentAircraft.ID}, Ticks Remaining: {TicksRemaining}");
            }            
        }
    }
}
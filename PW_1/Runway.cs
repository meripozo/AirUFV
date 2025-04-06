using System;

namespace AirportSimulation
{
    public class Runway
    {
        public string ID { get; set; }
        public RunwayStatus Status { get; set; }
        public Aircraft CurrentAircraft { get; set; }
        public int TicksRemaining { get; set; } // ticks until the runway is free

        public const int DefaultTicksAvailability = 3; // ticks it takes to land and free runway

        public Runway(string id)
        {
            ID = id;
            Status = RunwayStatus.Free;
            CurrentAircraft = null;
            TicksRemaining = 0;
        }

        //add request runway

        // Update the runway's occupation time for one tick.
        public void UpdateTick()
        {
            if (Status == RunwayStatus.Occupied)
            {
                if (TicksRemaining > 0)
                {
                    TicksRemaining--;
                }
                if (TicksRemaining == 0 && CurrentAircraft != null)
                {
                    // Landing complete and aircraft is OnGround
                    CurrentAircraft.Status = AircraftStatus.OnGround;
                    ReleaseRunway();
                }
            }
        }
        
        //add free runway for next aircraft

        public override string ToString()
        {
            return Status == RunwayStatus.Free
                ? $"{ID}: Free"
                : $"{ID}: Occupied by {CurrentAircraft.ID}, Ticks Remaining: ";
        }

        // Attempts to assign an aircraft to the runway for landing
        public bool RequestRunway(Aircraft aircraft)
        {
            if (Status == RunwayStatus.Free && aircraft.Status == AircraftStatus.Waiting)
            {
                CurrentAircraft = aircraft;
                Status = RunwayStatus.Occupied;
                TicksRemaining = DefaultTicksAvailability;
                aircraft.Status = AircraftStatus.Landing;
                return true;
            }
            return false;
        }

        // Frees the runway
        public void ReleaseRunway()
        {
            CurrentAircraft = null;
            Status = RunwayStatus.Free;
            TicksRemaining = 0;
        }
    }
}

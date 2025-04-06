using System;

namespace AirportSimulation
{
    public class Runway
    {
        public string ID { get; set; }
        public RunwayStatus Status { get; set; }
        public Aircraft CurrentAircraft { get; set; }
        //we need to add tick remaining atribute 


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
    }
}

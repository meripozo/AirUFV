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
        }

        //add request runway

        //add uodate runway
        //add release runway




        public override string ToString()
        {
            return Status == RunwayStatus.Free
                ? $"{ID}: Free"
                : $"{ID}: Occupied by {CurrentAircraft.ID}, Ticks Remaining: ";
        }
    }
}
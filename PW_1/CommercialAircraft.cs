using System;

namespace AirportSimulation
{
    public class CommercialAircraft : Aircraft //inherits from aircraft 
    {
        public int NumPassengers {get; set;} //individual attribute from commercial aircraft

        public CommercialAircraft(string id, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, int numPassengers)
            : base(id, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            NumPassengers = numPassengers;
        }

    }
}
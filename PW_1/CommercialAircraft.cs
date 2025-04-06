using System;

namespace AirportSimulation
{
    public class CommercialAircraft : Aircraft //inherits from aircraft 
    {
        public int NumPassengers {get; set;} //individual attribute from commercial aircraft

        public CommercialAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, int numPassengers)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            NumPassengers = numPassengers;
        }

        public override string ToString()
        {
            return $"[Commercial] {base.ToString()}, Passengers: {NumPassengers}";
        }
    }
}

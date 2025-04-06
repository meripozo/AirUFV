using System;

namespace AirportSimulation
{
    public class PrivateAircraft : Aircraft //inherits from aircraft 
    {
        public string Owner {get; set;} //individual attribute from private aircraft

        public PrivateAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, string owner)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            Owner = owner;
        }

        public override string ToString()
        {
            return $"[Private] {base.ToString()}, Owner: {Owner}";
        }
    }
}

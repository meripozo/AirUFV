using System;

namespace AirportSimulation
{
    public class CargoAircraft : Aircraft //inherits from aircraft 
    {
        public double MaxLoad {get; set;} //individual attribute from cargo aircraft

        public CargoAircraft(string id, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, double maxLoad)
            : base(id, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            MaxLoad = maxLoad;
        }

        public override string ToString()
        {
            return $"[Cargo] {base.ToString()}, Max Load: {MaxLoad}kg";
        }
    }
}

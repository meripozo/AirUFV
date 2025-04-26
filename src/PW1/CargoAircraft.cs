using System;

namespace AirportSimulation
{
    public class CargoAircraft : Aircraft
    {
        public double MaxLoad { get; set; }

        public CargoAircraft(string id, int status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, double maxLoad)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            MaxLoad = maxLoad;
        }

        public override void PrintAircraftInfo()
        {
            Console.Write($"[Cargo]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Max Load: {MaxLoad}kg");
        }
    }
}
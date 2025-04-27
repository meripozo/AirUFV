using System;

namespace AirportSimulation
{
    public class CargoAircraft : Aircraft
    {
        // this is a unique property for cargo aircrafts
        public double MaxLoad { get; set; }

        public CargoAircraft(string id, int status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, double maxLoad)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            MaxLoad = maxLoad;
        }

        // We Override the Print method from the aircraft class to include cargo-specific info
        public override void PrintAircraftInfo()
        {
            Console.Write($"[Cargo]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Max Load: {MaxLoad}kg");
        }
    }
}
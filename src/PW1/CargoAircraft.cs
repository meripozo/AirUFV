using System;

namespace AirportSimulation
{
    public class CargoAircraft : Aircraft
    {
        // this is a unique property for cargo aircrafts
        public double maxLoad { get; set; }

        public CargoAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, double maxLoad)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            this.maxLoad = maxLoad;
        }

        // We Override the Print method from the aircraft class to include cargo-specific info
        public override void PrintAircraftInfo()
        {
            Console.Write($"[Cargo]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Max Load: {this.maxLoad}kg");
        }
    }
}
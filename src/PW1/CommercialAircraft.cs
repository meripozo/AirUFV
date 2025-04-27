using System;

namespace AirportSimulation
{
    public class CommercialAircraft : Aircraft
    {
        // this is a unique property for commercial aircrafts
        public int numPassengers { get; set; }

        public CommercialAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, int numPassengers)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            this.numPassengers = numPassengers;
        }

        // We Override the Print method from the aircraft class to include cargo-specific info
        public override void PrintAircraftInfo()
        {
            Console.Write($"[Commercial]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Passengers: {this.numPassengers}");
        }
    }
}
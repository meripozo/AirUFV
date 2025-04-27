using System;

namespace AirportSimulation
{

    public class PrivateAircraft : Aircraft
    {
        // unique property for private aircrafts
        public string owner { get; set; }

        public PrivateAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, string owner)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            this.owner = owner;
        }
        
        // We Override the Print method from the aircraft class to include cargo-specific info
        public override void PrintAircraftInfo()
        {
            Console.Write($"[Private]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Owner: {this.owner}");
        }
    }
}
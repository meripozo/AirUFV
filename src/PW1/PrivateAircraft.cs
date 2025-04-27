using System;

namespace AirportSimulation
{
    // derived from verifications class
    public class PrivateAircraft : Aircraft
    {
        public string Owner { get; set; } // unique property for private aircrafts

        public PrivateAircraft(string id, int status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, string owner)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            Owner = owner;
        }

        // Overrides the Print method from the aircraft class to include private-specific logic
        public override void PrintAircraftInfo()
        {
            Console.Write($"[Private]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Owner: {Owner}");
        }
    }
}
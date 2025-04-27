using System;

namespace AirportSimulation
{
    public class CommercialAircraft : Aircraft
    {
        public int NumPassengers { get; set; } // unique property for commercial aircrafts

        public CommercialAircraft(string id, int status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, int numPassengers)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            NumPassengers = numPassengers;
        }

        // Overrides the Print method from the aircraft class to include commercial-specific logic
        public override void PrintAircraftInfo()
        {
            Console.Write($"[Commercial]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Passengers: {NumPassengers}");
        }
    }
}
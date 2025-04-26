using System;

namespace AirportSimulation
{
    public class CommercialAircraft : Aircraft
    {
        public int NumPassengers { get; set; }

        public CommercialAircraft(string id, int status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, int numPassengers)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            NumPassengers = numPassengers;
        }

        public override void PrintAircraftInfo()
        {
            Console.Write($"[Commercial]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Passengers: {NumPassengers}");
        }
    }
}
using System;

namespace AirportSimulation
{
    public class PrivateAircraft : Aircraft
    {
        public string Owner { get; set; }

        public PrivateAircraft(string id, int status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, string owner)
            : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            Owner = owner;
        }

        public override void PrintAircraftInfo()
        {
            Console.Write($"[Private]");

            base.PrintAircraftInfo();

            Console.WriteLine($", Owner: {Owner}");
        }
    }
}
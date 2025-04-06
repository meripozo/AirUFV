using System;
using System.Net;

namespace AirportSimulation
{
    public abstract class Aircraft 
    {
        public string ID{get; set;}
        public AircraftStatus Status {set; get;}
        public int Distance {get; set;}
        public int Speed {get; set;}
        public double FuelCapactity{get;set;}
        public double FuelConsumption {get; set;}
        public double CurrentFuel {get; set;}
    
//attributes for the aircrafts 
        public Aircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapactity, double fuelConsumption, double currentFuel)
        {
            ID = id;
            Status=status;
            Distance = distance;
            Speed = speed;
            FuelCapactity = fuelCapactity;
            FuelConsumption = fuelConsumption;
            CurrentFuel = currentFuel;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Status: {Status}, Distance: {Distance}km, Speed: {Speed}km/h, Fuel: {CurrentFuel}/{FuelCapactity}";
        }
    }
}
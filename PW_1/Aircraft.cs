using System;

namespace AirportSimulation
{
    public abstract class Aircraft
    {
        public string ID { get; set; }
        public int Status { get; set; }
        public int Distance { get; set; }          // in kilometers
        public int Speed { get; set; }             // in km/h
        public double FuelCapacity { get; set; }   // in liters
        public double FuelConsumption { get; set; } // liters per km
        public double CurrentFuel { get; set; }     // in liters

        public Aircraft(string id, int status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel)
        {
            ID = id;
            Status = status;
            Distance = distance;
            Speed = speed;
            FuelCapacity = fuelCapacity;
            FuelConsumption = fuelConsumption;
            CurrentFuel = currentFuel;
        }

        // Updates the aircraft's state for one tick (15 minutes)
        public virtual void UpdateTick()
        {
            if (Status == 1)
            {
                // Calculate distance covered in 15 minutes (speed / 4)
                int distanceCovered = Speed / 4;
                if (distanceCovered > Distance)
                {
                    distanceCovered = Distance;
                }
                Distance -= distanceCovered;

                // Consumed fuel according to distance covered
                double fuelUsed = distanceCovered * FuelConsumption;
                CurrentFuel -= fuelUsed; 
                if (CurrentFuel < 0) CurrentFuel = 0; // Avoid a negative fuel value

                // When the aircraft reaches the airport, set status to Waiting
                if (Distance == 0)
                {
                    Status = 2;
                }
            }
        }

        public virtual string ToString()
        {
            return $"ID: {ID}, Status: {Status}, Distance: {Distance}km, Speed: {Speed}km/h, Fuel: {CurrentFuel}/{FuelCapacity}L";
        }
    }
}

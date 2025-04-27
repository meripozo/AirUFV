using System;

namespace AirportSimulation
{
    public abstract class Aircraft
    {
        protected string id { get; set; }
        protected int status { get; set; }
        protected int distance { get; set; }          // in kilometers
        protected int speed { get; set; }             // in km/h
        protected double fuelCapacity { get; set; }   // in liters
        protected double fuelConsumption { get; set; } // liters per km
        protected double currentFuel { get; set; }     // in liters

        public Aircraft(string id, int status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel)
        {
            this.id = id;
            this.status = status;
            this.distance = distance;
            this.speed = speed;
            this.fuelCapacity = fuelCapacity;
            this.fuelConsumption = fuelConsumption;
            this.currentFuel = currentFuel;
        }
        public string GetID()
        {
            return this.id;
        }
        public int GetStatus()
        {
            return this.status;
        }
        public void SetStatus()
        {
            this.status = status;
        }
       


        // Updates the aircraft's state for one tick (15 minutes)
        public virtual void UpdateTick()
        {
            if (status == 1)
            {
                // Calculate distance covered in 15 minutes (speed / 4)
                int distanceCovered = speed / 4;
                if (distanceCovered > distance)
                {
                    distanceCovered = distance;
                }
                distance -= distanceCovered;

                // Consumed fuel according to distance covered
                double fuelUsed = distanceCovered * fuelConsumption;
                currentFuel -= fuelUsed; 
                if (currentFuel < 0) currentFuel = 0; // Avoid a negative fuel value

                // When the aircraft reaches the airport, set status to Waiting
                if (distance == 0)
                {
                    status = 2;
                }
            }
        }

        // Here we use create this virtual funcion, in order to override it when we print aircrafts info, 
        // depending on the type
        public virtual void PrintAircraftInfo()
        {
            Console.Write($" ID: {id}, Status: {status}, Distance: {distance}km, Speed: {speed}km/h, Fuel: {currentFuel}/{fuelCapacity}L");
        }
    }
}
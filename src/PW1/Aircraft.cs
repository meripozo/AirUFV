using System;

namespace AirportSimulation
{
    public abstract class Aircraft
    {
        protected string id { get; set; }
        protected AircraftStatus status;
        protected int distance { get; set; }          // in kilometers
        protected int speed { get; set; }             // in km/h
        protected double fuelCapacity { get; set; }   // in liters
        protected double fuelConsumption { get; set; } // liters per km
        protected double currentFuel { get; set; }     // in liters


        // we use enumerators for aircraft status and runway status
        // aircraft status has type int assigned for easier use on program
        public enum AircraftStatus : int
        {
            InFlight = 1,
            Waiting = 2,
            Landing = 3,
            OnGround = 4
        }

        public Aircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel)
        {
            this.id = id;
            this.status = status;
            this.distance = distance;
            this.speed = speed;
            this.fuelCapacity = fuelCapacity;
            this.fuelConsumption = fuelConsumption;
            this.currentFuel = currentFuel;
        }

        //with the enums, it only worked this way, with getters and setters
        public AircraftStatus GetStatus()
        {
            return this.status;
        }
        public void SetStatus(AircraftStatus status)
        {
            this.status = status;
        }

        public string GetId()
        {
            return this.id;
        }
       


        // Updates the aircraft's state for one tick (15 minutes)
        public virtual void UpdateTick()
        {
            if (status == Aircraft.AircraftStatus.InFlight)
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
                    status = Aircraft.AircraftStatus.Waiting;
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
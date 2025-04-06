using System;

namespace AirportSimulation
{
    public abstract class Airport
    {
        public List<Aircraft> Aircrafts {get; set;}
        //once we create the runway class, we have to create a runway list

        public Airport()
        {
            Aircrafts = new List<Aircraft>();
            //add runway list
        }

        public void ShowStatus() //al ser void creo que no devuelve nada no?
        {
            //show aircraft status 
            Console.WriteLine("\nAircraft Status:");
            foreach (var aircraft in Aircrafts)
            {
                Console.WriteLine(aircraft.ToString());
            }

            //add runway status 
        }

        //create class for the 15 minute tick

        //create class load from file

        //create load 
    }
}

using System;
namespace AirportSimulation{
    public class Program{
        public static void Main(string[] args)
        {
            Simulator simulator = new Simulator(); //we instantiate Simulator, our "controler class"
            simulator.MainMenu();       
        }
    }
}
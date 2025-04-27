using System;

namespace AirportSimulation
{
    public class ValidString : Verifications
    {
        public override bool validateInput(string input)
        {
            if (input == null || input.Trim().Length == 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-empty string.");
                return false;
            }    
            return true;
        }
    }
}
using System;

namespace AirportSimulation
{
    public class ValidString : Verifications
    {
        // Override validateInput method to check if string input is valid
        public override bool validateInput(string input)
        {
            // check if input is null or empty
            if (input == null || input.Trim().Length == 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-empty string.");
                return false;
            }    
            return true;
        }
    }
}
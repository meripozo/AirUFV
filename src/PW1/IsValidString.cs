using System;

namespace AirportSimulation
{
    public class ValidString : Verifications
    {
        // We override validateInput method to check if double input is valid
        public override bool validateInput(string input)
        {
            // Here we check if input is null or empty
            if (input == null || input.Trim().Length == 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-empty string.");
                return false;
            }    
            return true;
        }
    }
}
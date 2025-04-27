using System;

namespace AirportSimulation
{
    // derived from verifications class
    public class ValidInt : Verifications
    {
        // Override validateInput method to check if int input is valid
        public override bool validateInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                // check if int is negative
                if (input[i] < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative integer.");
                    return false;
                }
                // check if input is a digit
                else if( !char.IsDigit(input[i]))
                {
                    Console.WriteLine("Invalid input. Please enter a digit.");
                    return false;
                }
            }
            return true;
        }
    }
}
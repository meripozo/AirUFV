using System;

namespace AirportSimulation
{
    // we create ValidDouble. It derives from verifications class
    public class ValidDouble : Verifications
    {
        // We override validateInput method to check if double input is valid
        public override bool validateInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                // check if double is negative
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
using System;

namespace AirportSimulation
{
    // we create ValidInt. It derives from verifications class
    public class ValidInt : Verifications
    {
        // We override validateInput method to check if double input is valid
        public override bool validateInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                // check if double is negative
                if (input[i] == '-' && i == 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative integer.");
                    Console.ReadLine();
                    return false;
                }
                else if (input == null || input == "")
                {
                    Console.WriteLine("Invalid input. Please enter a digit.");
                    Console.ReadLine();
                    return false;
                }
                // check if input is a digit
                else if( !char.IsDigit(input[i]))
                {
                    Console.WriteLine("Invalid input. Please enter a digit.");
                    Console.ReadLine();
                    return false;
                }
            }
            return true;
        }
    }
}
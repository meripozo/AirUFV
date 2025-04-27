using System;

namespace AirportSimulation
{
    public class ValidInt : Verifications
    {
        public override bool validateInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative integer.");
                    return false;
                }
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
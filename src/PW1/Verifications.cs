using System;

namespace AirportSimulation
{
    public abstract class Verifications
    {
        public abstract bool validateInput(string input);











        // public static bool IsValidString(string input)
        // {
        //     if (input == null || input.Trim().Length == 0)
        //     {
        //         Console.WriteLine("Invalid input. Please enter a non-empty string.");
        //         return false;
        //     }
            
        //     return true;
        // }

        // public static bool IsValidInt(int input)
        // {
        //     if (input < 0)
        //     {
        //         Console.WriteLine("Invalid input. Please enter a non-negative integer.");
        //         return false;
        //     }
            
        //     return true;
        // }

        // public static bool IsValidDouble(double input)
        // {
        //     if (input < 0)
        //     {
        //         Console.WriteLine("Invalid input. Please enter a non-negative number.");
        //         return false;
        //     }
            
        //     return true;
        // }
    }
}


using System;

namespace AirportSimulation
{
    //We use this abstract class for the exceptions and errors control. We override the
    //validateInput bool function.
    public abstract class Verifications
    {
        public abstract bool validateInput(string input);
    }
}

using System;

namespace AirportSimulation
{
    //states for the aircraft and the runway 
    public enum AircraftStatus 
    {
        InFlight,
        Waiting,
        Landing,
        OnGround
    }

    public enum RunwayStatus
    {
        Free,
        Occupied
    }
}

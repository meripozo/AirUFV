using System;

namespace AirportSimulation
{
    // we use enumerators for aircraft status and runway status
    // aircraft status has type int assigned for easier use on program
    public enum AircraftStatus : int
    {
        InFlight = 1,
        Waiting = 2,
        Landing = 3,
        OnGround = 4
    }
    // no type assigned as considered unecessary for runway status
    public enum RunwayStatus
    {
        Free,
        Occupied
    }
}
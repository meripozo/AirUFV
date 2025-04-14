using System;

namespace AirportSimulation
{
    public enum AircraftStatus : int
    {
        InFlight = 1,
        Waiting = 2,
        Landing = 3,
        OnGround = 4
    }

    public enum RunwayStatus
    {
        Free,
        Occupied
    }
}
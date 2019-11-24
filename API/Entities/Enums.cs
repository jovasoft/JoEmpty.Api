using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public enum Currencies : byte
    {
        TL,
        EURO,
        DOLLAR
    }

    public enum Supplies : byte
    {
        Internal,
        External
    }

    public enum MaintenanceStatuses : byte
    {
        Active,
        Passive
    }

    public enum FacilityTypes : byte
    {
        MR,
        MRL,
        Hydraulic,
        DumbWaiter,
        MW,
        ESC
    }
}

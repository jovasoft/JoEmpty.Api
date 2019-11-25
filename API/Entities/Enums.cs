using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public enum Currencies : byte
    {
        TL = 1,
        EURO,
        DOLLAR
    }

    public enum Supplies : byte
    {
        Internal = 1,
        External
    }

    public enum MaintenanceStatuses : byte
    {
        Active = 1,
        Passive
    }

    public enum FacilityTypes : byte
    {
        MR = 1,
        MRL,
        Hydraulic,
        DumbWaiter,
        MW,
        ESC
    }
}

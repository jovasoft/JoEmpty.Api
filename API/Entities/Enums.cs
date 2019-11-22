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

    public enum MaintenanceStatuses
    {
        Active,
        Passive
    }

    public enum UnitTypes
    {
        MR,
        MRL,
        Hydraulic,
        DumbWaiter,
        MW,
        ESC
    }
}

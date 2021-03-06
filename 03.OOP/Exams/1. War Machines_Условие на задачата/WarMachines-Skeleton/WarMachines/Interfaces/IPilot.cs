﻿using System.Collections;
using System.Collections.Generic;

namespace WarMachines.Interfaces
{
    public interface IPilot
    {
        string Name { get; }
       
        void AddMachine(IMachine machine);

        string Report();
    }
}

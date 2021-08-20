﻿using System.Collections.Generic;

namespace InventorySystem
{
    public sealed class ContainerComparer : IComparer<Container>
    {
        public int Compare(Container x, Container y)
        {
            if (x.Order > y.Order) return -1;
            else if (x.Order < y.Order) return 1;
            return 0;
        }
    }
}
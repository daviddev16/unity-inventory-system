using System.Collections.Generic;

namespace InventorySystem.Internals
{
    public sealed class ContainerComparer : IComparer<Container>
    {

        public static readonly ContainerComparer CONTAINER_COMPARER = new ContainerComparer();

        public int Compare(Container x, Container y)
        {
            if (x.GetOrder() > y.GetOrder()) return 1;
            else if (x.GetOrder() < y.GetOrder()) return -1;
            return 0;
        }
    }
}
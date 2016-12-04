using Problem1.Domain;
using System.Collections.Generic;

namespace Problem1.Test
{
    internal class TripCardComparer : IEqualityComparer<TripCard>
    {
        public TripCardComparer(IEqualityComparer<string> tripCardStringComparer)
        {
            StringComparer = tripCardStringComparer;
        }

        public IEqualityComparer<string> StringComparer { get; private set; }

        public bool Equals(TripCard x, TripCard y)
        {
            return StringComparer.Equals(x.Source, y.Source) &&
                StringComparer.Equals(x.Destination, y.Destination);
        }

        public int GetHashCode(TripCard obj) => obj.GetHashCode();
    }
}

using System.Collections.Generic;
using Player.Weapons.Base;

namespace Comparers
{
    public class WeaponComparer : IComparer<Weapon>
    {
        public int Compare(Weapon first, Weapon second)
        {
            if (first is null || second is null || first.Price == second.Price) return 0;

            return first.Price > second.Price ? 1 : -1;
        }
    }
}

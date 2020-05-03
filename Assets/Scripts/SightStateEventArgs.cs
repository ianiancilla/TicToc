using System;

namespace TicToc.Mechanics
{
    public class SightStateEventArgs : EventArgs
    {
        public readonly bool InSight;

        public SightStateEventArgs(bool inSight)
        {
            InSight = inSight;
        }
    }
}
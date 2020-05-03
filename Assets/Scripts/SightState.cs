using System;
using UnityEngine;

namespace TicToc.Mechanics
{
    public class SightState : MonoBehaviour
    {
        private bool inSight;

        public event EventHandler<SightStateEventArgs> SightStateChanged;

        public void SetSightState(bool inSight)
        {
            if(this.inSight == inSight)
            {
                return;
            }

            this.inSight = inSight;
            SightStateChanged?.Invoke(this, new SightStateEventArgs(inSight));
        }
    }
}
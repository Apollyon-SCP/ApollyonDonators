using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ApollyonDonators.HatSystem
{
    internal class HatVisibility : NetworkBehaviour
    {
        [TargetRpc]
        public void TargetSetVisible(NetworkConnection target, bool visible)
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = visible;
            }
        }
    }
}

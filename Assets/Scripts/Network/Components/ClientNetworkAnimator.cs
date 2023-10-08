using Unity.Netcode.Components;
using UnityEngine;

namespace NetcodeVRTemplate.Network.Components
{
    [DisallowMultipleComponent]
    public class ClientNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
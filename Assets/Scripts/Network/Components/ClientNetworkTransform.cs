using Unity.Netcode.Components;
using UnityEngine;

namespace NetcodeVRTemplate.Network.Components
{
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace NetcodeVRTemplate.Network.Components
{
    [RequireComponent(typeof(XRBaseInteractable)), RequireComponent(typeof(NetworkObject))]
    public class ClientInteractableOwnership : NetworkBehaviour
    {
        private XRBaseInteractable _interactable;

        private void Awake()
        {
            _interactable = GetComponent<XRBaseInteractable>();
            if (_interactable == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Interactable not found on " + gameObject.name);
#endif
                enabled = false;
            }
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            _interactable.selectEntered.AddListener(ChangeOwnership);
        }
        
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            _interactable.selectEntered.RemoveListener(ChangeOwnership);
        }
        
        private void ChangeOwnership(SelectEnterEventArgs args)
        {
            if (IsOwner)
            {
                return;
            }
            
            ChangeOwnershipServerRpc(NetworkManager.Singleton.LocalClientId);
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void ChangeOwnershipServerRpc(ulong clientId)
        {
            NetworkObject.ChangeOwnership(clientId);
        }
    }
}

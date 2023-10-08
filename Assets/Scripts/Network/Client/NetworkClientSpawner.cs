using Unity.Netcode;
using UnityEngine;

namespace NetcodeVRTemplate.Network.Client
{
    public class NetworkClientSpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject networkClient;

        public override void OnNetworkSpawn()
        {
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnPlayerServerRpc(ulong clientId)
        {
            Instantiate(networkClient).SpawnAsPlayerObject(clientId);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (NetworkManager.Singleton is not null)
            {
                NetworkManager.Singleton.Shutdown();
            }
        }

        [ContextMenu("Start Host")]
        public void StartHost()
        {
            if (NetworkManager.Singleton is not null)
            {
                NetworkManager.Singleton.StartHost();
            }
        }

        [ContextMenu("Start Client")]
        public void StartClient()
        {
            if (NetworkManager.Singleton is not null)
            {
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

namespace HelloWorld
{
    public class NetworkSettings : MonoBehaviour
    {
        public void ButtonHostPress()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.StopServer();
            }

            if (NetworkManager.Singleton.IsClient)
            {
                NetworkManager.Singleton.StopClient();
            }

            if (!NetworkManager.Singleton.IsHost)
            {
                NetworkManager.Singleton.StartHost();
            }
        }

        public void ButtonClientPress()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.StopServer();
            }
            
            if (NetworkManager.Singleton.IsHost)
            {
                NetworkManager.Singleton.StopHost();
            }

            if (!NetworkManager.Singleton.IsClient)
            {
                NetworkManager.Singleton.StartClient();
            }
        }

        public void ButtonServerPress()
        {
            if (NetworkManager.Singleton.IsHost)
            {
                NetworkManager.Singleton.StopHost();
            }
            
            if (NetworkManager.Singleton.IsClient)
            {
                NetworkManager.Singleton.StopClient();
            }

            if (!NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.StartServer();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.MobaRPG.Utils
{
    public class NetworkObjectSpawner : MonoBehaviour
    {
        public NetworkObject prefabReference;

        public void Awake()
        {
            if (NetworkManager.Singleton && NetworkManager.Singleton.IsServer &&
                NetworkManager.Singleton.SceneManager != null)
            {
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManagerOnLoadEventCompleted;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            if (NetworkManager.Singleton && NetworkManager.Singleton.IsServer &&
                NetworkManager.Singleton.SceneManager != null)
            {
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= SceneManagerOnLoadEventCompleted;
            }
        }

        void SceneManagerOnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
        {
            SpawnNetworkObject();
            Destroy(gameObject);
        }

        void SpawnNetworkObject()
        {
            var instantiatedNetworkObject = Instantiate(prefabReference, transform.position, transform.rotation, null);
            SceneManager.MoveGameObjectToScene(instantiatedNetworkObject.gameObject,
                SceneManager.GetSceneByName(gameObject.scene.name));
            instantiatedNetworkObject.transform.localScale = transform.lossyScale;
            instantiatedNetworkObject.Spawn(destroyWithScene: true);
        }
    }
}
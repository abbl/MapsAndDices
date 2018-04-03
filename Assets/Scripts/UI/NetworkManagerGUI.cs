using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(NetworkManager))]
public class NetworkManagerGUI : MonoBehaviour
{
    private NetworkManager networkManager;

    private void Awake()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

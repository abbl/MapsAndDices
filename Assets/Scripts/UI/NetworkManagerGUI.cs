using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(NetworkManager))]
public class NetworkManagerGUI : MonoBehaviour
{
    private NetworkManager networkManager;
    public Text networkAddressField;
    public Canvas networkManagerCanvas;
    public Canvas inGameCanvas;

    private void Awake()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    private void Start()
    {
        networkManagerCanvas.gameObject.SetActive(true);

        if (inGameCanvas != null)
            inGameCanvas.gameObject.SetActive(false);
    }

    public void Host()
    {
        if (IsAnyNetworkServiceStarted())
        {
            networkManager.StartHost();
        }
    }

    public void Connect()
    {
        if (IsAnyNetworkServiceStarted())
        {
            SetNetworkAddress();
            ShowConnectingWindow();
            networkManager.StartClient();
        }
    }

    public void StopConnection()
    {
        networkManager.StopClient();
        HideConnectingWindow();
    }

    private bool IsAnyNetworkServiceStarted()
    {
        return !networkManager.isNetworkActive;
    }

    private void SetNetworkAddress()
    {
        networkManager.networkAddress = networkAddressField.text;
    }

    private void ShowConnectingWindow()
    {
        networkManagerCanvas.transform.Find("ConnectionStatus").gameObject.SetActive(true);
    }

    private void HideConnectingWindow()
    {
        networkManagerCanvas.transform.Find("ConnectionStatus").gameObject.SetActive(false);
    }

    private void Update()
    {
        SwapCanvasAfterConnection();
    }

    private void SwapCanvasAfterConnection()
    {
        if (networkManager.isNetworkActive)
        {
            if (networkManager.client.isConnected)
            {
                HideCanvas();
                ShowInGameCanvas();
                gameObject.SetActive(false);
            }
        }
    }

    private void HideCanvas()
    {
        networkManagerCanvas.gameObject.SetActive(false);
    }

    private void ShowInGameCanvas()
    {
        inGameCanvas.gameObject.SetActive(true);
    }
}

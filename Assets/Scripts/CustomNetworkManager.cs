using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{

    [SerializeField]
    Text IPAddress;

    [Header("Menus")]

    [SerializeField]
    Canvas EscMenu;

    [SerializeField]
    Canvas NetworkMenu;

    void Start()
    {
        NetworkMenu.enabled = true;
    }

    public void StartNewHost()
    {
        SetIP();
        SetPort();
        NetworkManager.singleton.StartHost();
        NetworkMenu.enabled = false;
    }

    public void StartNewClient()
    {
        SetIP();
        SetPort();
        NetworkManager.singleton.StartClient();
        NetworkMenu.enabled = false;
    }

    void SetIP()
    {
        string _IPAddress = IPAddress.text;
        if (_IPAddress == "")
            _IPAddress = "localhost";

        NetworkManager.singleton.networkAddress = _IPAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    public void DisconnectFromServer()
    {
        NetworkManager.singleton.StopHost();
        NetworkMenu.enabled = true;
        EscMenu.enabled = false;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("Connected");
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
        Debug.Log("Client error " + errorCode.ToString());
        SceneManager.LoadScene("MainScene");
    }

    public override void OnClientDisconnect(NetworkConnection c)
    {
        Debug.Log("Client disconnect");
        DisconnectFromServer();
    }
}

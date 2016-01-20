using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RespawnScript : NetworkBehaviour
{

    [Header("Camera")]

    [SerializeField]
    Camera worldCam;

    void Start()
    {
        GameManager.respawnScript = this;
    }
    bool respawning = false;

    public void respawn(GameObject oldPlayer)
    {
        if (!respawning)
        {
            respawning = true;
            StartCoroutine(respawnTimer(oldPlayer));
        }
    }

    IEnumerator respawnTimer(GameObject oldPlayer)
    {
        oldPlayer.SetActive(false);
        yield return new WaitForSeconds(0f); // No timeout currently
        CmdSpawnPlayer(oldPlayer);
        Debug.Log("Respawned");
        respawning = false;
    }


    [Command]
    void CmdSpawnPlayer(GameObject oldPlayer)
    {
        Transform spawnPos = NetworkManager.singleton.GetStartPosition();
        GameObject newPlayer = (GameObject)Instantiate(NetworkManager.singleton.playerPrefab, spawnPos.position, spawnPos.rotation);
        NetworkServer.Destroy(oldPlayer);
        NetworkServer.ReplacePlayerForConnection(oldPlayer.GetComponent<NetworkIdentity>().connectionToClient, newPlayer, oldPlayer.GetComponent<NetworkIdentity>().playerControllerId);

    }
}

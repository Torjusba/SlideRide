using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [Header("Respawn")]

    Canvas respawnMenu;

    Camera sceneCamera;

    void Start()
    {

        gameObject.name = "Player " + GetComponent<NetworkIdentity>().netId;

        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();

        }
        else
        {

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayer(netID, player);
    }

    void AssignRemoteLayer()
    {
        AssignLayer(transform, LayerMask.NameToLayer(remoteLayerName));
    }

    void AssignLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
        {
            AssignLayer(child, layer);
        }
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void OnDisable()
    {
        if (!this.isLocalPlayer)
        {
            return;
        }

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameManager.UnRegisterPlayer(transform.name);
    }


}

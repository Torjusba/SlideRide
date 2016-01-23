using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class BulletScript : NetworkBehaviour
{
    [SerializeField]
    GameObject killMessagePrefab;

    [SerializeField]
    GameObject fragment;

    public Player owner;
    public Player target;
    public int range;
    public Vector3 direction;
    public float velocity;

    void Start()
    {
        if (!base.isServer)
            return;
        
        GetComponent<Rigidbody>().velocity += direction * velocity;
        Destroy(gameObject, range); // Destroys the object after range seconds.
    }

    [Server]
    void OnCollisionEnter(Collision collision)
    {
        if (base.isServer)
        {
            bool destroyBullet = true;
            //If it hits a player, kill it
            if (collision.collider.tag == "Player")
            {
                target = collision.collider.GetComponentInParent<Player>();
                // target = GameManager.GetPlayer(collision.collider.name);

                //If the target is not the player who fired, kill
                if (target != null && owner != null && target != owner)
                {
                    target.Die();

                    CmdShowKillMessage();
                }
                else
                {
                    destroyBullet = false;
                }
            }

            if (destroyBullet)
            {
                //Create fragments
                for (int i = 0; i < 8; i++)
                {
                    Vector3 fragmentPosition = transform.position;
                    fragmentPosition += Random.rotation.eulerAngles.normalized * 0.1f;

                    CmdCreateFragment(fragmentPosition);
                }
                Destroy(gameObject);
            }
        }
    }


    [Command]
    void CmdShowKillMessage()
    {
        string ownerName = owner.GetComponent<Player>().playerName;
        string targetName = target.GetComponent<Player>().playerName;

        GameObject killMessage = (GameObject)Instantiate(killMessagePrefab);
        KillMessageScript kms = killMessage.GetComponent<KillMessageScript>();
        kms.owner = ownerName;
        kms.target = targetName;
        RpcShowKillMessage(ownerName, targetName);
    }

    [ClientRpc]
    void RpcShowKillMessage(string ownerName, string targetName)
    {
        GameObject killMessage = (GameObject)Instantiate(killMessagePrefab);
        KillMessageScript kms = killMessage.GetComponent<KillMessageScript>();
        kms.owner = ownerName;
        kms.target = targetName;

    }

    [Command]
    void CmdCreateFragment(Vector3 position)
    {
        GameObject obj = Instantiate(fragment, position, transform.rotation) as GameObject;
        Destroy(obj, 2f);
        NetworkServer.Spawn(obj);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class BulletScript : NetworkBehaviour
{

    [SerializeField]
    GameObject fragment;

    public int damage;
    public int range;
    public Vector3 direction;
    public float velocity;

    void Start()
    {
        if (!base.isServer)
            return;

        StartCoroutine(timeBullet());
        GetComponent<Rigidbody>().velocity += direction * velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        //If it hits a player, do damage
        if (collision.collider.tag == "Player")
        {
            Player target = GameManager.GetPlayer(collision.collider.name);
            target.TakeDamage(damage);
        }

        //Create fragments
        for (int i = 0; i < 7; i++)
        {
            Vector3 fragmentPosition = transform.position;
            fragmentPosition += Random.rotation.eulerAngles.normalized * 0.1f;

            Instantiate(fragment, fragmentPosition, transform.rotation);
        }
        Destroy(gameObject);
    }

    IEnumerator timeBullet()
    {
        while (true)
        {
            if (range <= 0)
            {
                Destroy(gameObject);
            }
            range--;
            yield return new WaitForSeconds(1f);
        }
    }
}

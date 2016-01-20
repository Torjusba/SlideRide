using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FireWeapon : NetworkBehaviour
{

    [SerializeField]
    private Transform barrel;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private PlayerWeapon weapon;

    [SerializeField]
    private AudioClip blasterSound;

    public AudioSource audioSource;

    bool hasFiredRecently = false;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetAxis("FireJoystick") > 0)
        {
            if (!hasFiredRecently)
            {
                StartCoroutine(Fire());
            }
        }
    }

    IEnumerator Fire()
    {
        hasFiredRecently = true;
        Cmdfire();
        PlaySound();
        yield return new WaitForSeconds(0.1f);
        hasFiredRecently = false;
        
    }
    
    public void PlaySound()
    {
        audioSource.pitch = Random.Range(0.7f, 1.5f);
        audioSource.PlayOneShot(blasterSound);
    }

    [Command]
    void Cmdfire()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, barrel.position + 2 * barrel.forward, barrel.rotation);
        bullet.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.owner = gameObject.GetComponent<Player>();
        bulletScript.range = weapon.range;
        bulletScript.direction = barrel.forward;
        bulletScript.velocity = weapon.muzzleVelocity;
        NetworkServer.Spawn(bullet);
    }
}

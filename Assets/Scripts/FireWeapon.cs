using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FireWeapon : NetworkBehaviour {

    [SerializeField]
    private Transform barrel;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private PlayerWeapon weapon;

	void Update () {
	    if (Input.GetButtonDown("Fire1"))
        {
            Cmdfire();
        }
	}

    [Command]
    void Cmdfire()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, barrel.position + barrel.forward * 2, barrel.rotation);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.range = weapon.range;
        bulletScript.damage = weapon.damage;
        bulletScript.direction = barrel.forward;
        bulletScript.velocity = weapon.muzzleVelocity;
        NetworkServer.Spawn(bullet);
    }
}

using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    [SerializeField]
    PlayerMotor motor;


    public bool isAlive;

    void Awake()
    {
        isAlive = true;
    }

    void LateUpdate()
    {
        //If player falls off map
        if (transform.position.y <= -5)
        {
            isAlive = false;
        }

        //If player is no longer alive
        if (!isAlive)
        {
            motor.die();
        }
    }

    public void Die()
    {
        isAlive = false;
    }
}

using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    public string playerName;

    [SerializeField]
    PlayerMotor motor;

    Camera mainCamera;

    public bool isAlive;
    
    void Awake()
    {
        mainCamera = gameObject.GetComponentInChildren<Camera>();
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

using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

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

    void OnGUI()
    {
        Vector2 playerPosOnScreen = mainCamera.WorldToScreenPoint(gameObject.GetComponent<Transform>().position);

        GUI.Label(new Rect(playerPosOnScreen.x - 10, playerPosOnScreen.y - 10, 20, 20), name);
    }
}

using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    [SerializeField]
    PlayerMotor motor;

    [SerializeField]
    public int maxHealth = 100;

    [SyncVar]
    public int currentHealth;

    void Awake()
    {
        SetDefaults();
    }

    void FixedUpdate()
    {
        if (transform.position.y <= -5)
        {
            TakeDamage(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        //If no health left, die
        if (currentHealth <= 0)
        {
            motor.die();
        }
    }



    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }
}

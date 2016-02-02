using UnityEngine;
using System.Collections;

public class PowerupHandler : MonoBehaviour
{
    PowerupType powerup;

    Vector3 initialPosition;
    // Use this for initialization
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float height = 0.2f * Mathf.Sin(Time.time * 1.5f);
        Vector3 newPos = initialPosition + new Vector3(0f, height, 0f);

        transform.Rotate(0f, 30f * Time.deltaTime, 0f);
        transform.localPosition = newPos;
    }

    public void SetPowerup(PowerupType type)
    {
        powerup = type;
    }

    public PowerupType GetPowerup()
    {
        return powerup;
    }
}

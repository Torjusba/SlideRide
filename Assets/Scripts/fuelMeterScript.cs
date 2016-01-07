using UnityEngine;
using System.Collections;

public class fuelMeterScript : MonoBehaviour {

    [SerializeField]
    MeshRenderer[] display;

    [SerializeField]
    PlayerMotor motor;

    [SerializeField]
    Material[] materials;

    float oldFuel;

	// Use this for initialization
	void Start () {
	    foreach (MeshRenderer r in display)
        {
            r.material = materials[1]; //On state
        }
        oldFuel = motor.fuel;
	}
	
	void Update () {
        float fuel = motor.fuel;
        if (fuel != oldFuel)
        {
            //Reset to off state
            foreach (MeshRenderer r in display)
            {
                r.material = materials[0];
            }
            float fuelPerBar = motor.maxFuel / 4;
            int barsOn = (int)Mathf.Floor(fuel / fuelPerBar);
            for(int i = 0; i <= barsOn; i++)
            {
                display[i].material = materials[1]; //On state
            }
        }
        oldFuel = fuel;
    }
}

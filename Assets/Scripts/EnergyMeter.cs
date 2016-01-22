using UnityEngine;
using System.Collections;

public class EnergyMeter : MonoBehaviour
{

    [SerializeField]
    MeshRenderer[] display;

    [SerializeField]
    EnergyContainer battery;

    [SerializeField]
    Material[] materials;

    float oldEnergy;

    // Use this for initialization
    void Start()
    {
        foreach (MeshRenderer r in display)
        {
            r.material = materials[1]; //On state
        }
        oldEnergy = battery.GetEnergyPercentage();
    }

    void Update()
    {
        float energy = battery.GetEnergyPercentage();
        if (energy != oldEnergy)
        {
            int barsOn = Mathf.RoundToInt(energy * display.Length);

            for (int i = 0; i < display.Length; i++)
            {
                if (i < barsOn)
                {
                    display[i].material = materials[1];
                }
                else
                {
                    display[i].material = materials[0];
                }
            }
        }
        oldEnergy = energy;
    }
}

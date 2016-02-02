using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PowerupManager : MonoBehaviour
{

    private int powerupsSpawned;
    List<PowerupLocation> points;

    public int TotalCountLimit;
    public Dictionary<PowerupType, int> PowerupCountLimits;
    // TODO MaxPowerupsInPlay (including powerups active on players)

    // Use this for initialization
    void Start()
    {
        points = new List<PowerupLocation>();
        foreach (PowerupLocation t in GetComponentsInChildren<PowerupLocation>())
        {
            points.Add(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountActivePowerups();
        DistributePowerups();
    }

    private void CountActivePowerups()
    {
        powerupsSpawned = 0;
        foreach (PowerupLocation l in points)
        {
            if (l.GetCurrentPowerup() != null)
                powerupsSpawned++;
        }
    }

    private void DistributePowerups()
    {

    }
}

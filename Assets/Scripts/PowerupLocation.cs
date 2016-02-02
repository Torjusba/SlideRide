using System;
using UnityEngine;

public class PowerupLocation : MonoBehaviour
{
    private PowerupHandler currentPowerup;
    private float timeLeftBeforeCanSpawn;

    [SerializeField]
    private AllowedPowerupSpawnInfo[] AllowedPowerups;


    public float SpawnDelay;

    public bool CanSpawnPowerup()
    {
        return GetCurrentPowerup() == null && timeLeftBeforeCanSpawn <= 0f;
    }

    public AllowedPowerupSpawnInfo[] GetAllowedPowerups()
    {
        AllowedPowerupSpawnInfo[] types;
        if (AllowedPowerups == null || AllowedPowerups.Length == 0)
        {
            PowerupType[] alltypes = Enum.GetValues(typeof(PowerupType)) as PowerupType[];
            types = new AllowedPowerupSpawnInfo[alltypes.Length];
            for (int i = 0; i < types.Length; i++)
            {
                types[i] = new AllowedPowerupSpawnInfo(alltypes[i], 1f);
            }
        }
        else
        {
            types = AllowedPowerups.Clone() as AllowedPowerupSpawnInfo[];
        }
        return types;
    }

    public PowerupType? GetCurrentPowerup()
    {
        if (currentPowerup == null)
            return null;
        else
            return currentPowerup.GetPowerup();
    }
}
using System;

[Serializable]
public class AllowedPowerupSpawnInfo
{

    public bool Allowed;
    public PowerupType Type;
    public float Probability;

    public AllowedPowerupSpawnInfo()
    {
        Allowed = true;
        Type = PowerupType.SpeedBoost;
        Probability = 1f;
    }

    public AllowedPowerupSpawnInfo(PowerupType powerup, float probability)
    {
        Type = powerup;
        Probability = (probability < 0 ? 0 : probability);
        Allowed = (probability > 0);
    }
}

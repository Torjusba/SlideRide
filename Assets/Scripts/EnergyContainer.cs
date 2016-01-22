using UnityEngine;
using System.Collections;
using System;

public class EnergyContainer : MonoBehaviour
{
    public float energy;
    public float maxEnergy;

    public float jetpackEnergyUsage;
    public float gravityEnergyUsage;
    public float jetpackIgnitionEnergyUsage;
    public float gravityIgnitionEnergyUsage;

    public float jetpackCooldownMinEnergy;
    public float gravityCooldownMinEnergy;

    public float energyRefillRate;

    // State variables
    private bool m_jetpackNeedsCooldown;
    private bool m_gravityNeedsCooldown;

    private bool m_jetpackEnabled;
    private bool m_gravityEnabled;

    void Start()
    {
        m_jetpackNeedsCooldown = false;
        m_gravityNeedsCooldown = false;
        energy = maxEnergy;
    }


    void FixedUpdate()
    {
        bool usedEnergy = false;

        if (m_jetpackNeedsCooldown)
            if (energy > jetpackCooldownMinEnergy)
                m_jetpackNeedsCooldown = false;
        if (m_gravityNeedsCooldown)
            if (energy > gravityCooldownMinEnergy)
                m_gravityNeedsCooldown = false;

        if (m_jetpackEnabled && HasEnergyForJetpack())
        {
            energy -= jetpackEnergyUsage * Time.fixedDeltaTime;
            usedEnergy = true;
        }
        if (m_gravityEnabled && HasEnergyForGravity())
        {
            energy -= gravityEnergyUsage * Time.fixedDeltaTime;
            usedEnergy = true;
        }

        if (m_jetpackEnabled && !HasEnergyForJetpack())
        {
            m_jetpackEnabled = false;
            m_jetpackNeedsCooldown = true; // We used all the energy, enable cooldown period
        }
        if (m_gravityEnabled && !HasEnergyForGravity())
        {
            m_gravityEnabled = false;
            m_gravityNeedsCooldown = true; // Cooldown
        }
        
        if (!usedEnergy && energy < maxEnergy) // Only refill energy if we didn't use any
            energy += energyRefillRate * Time.fixedDeltaTime;
    }


    public float GetEnergyPercentage()
    {
        return energy / maxEnergy;
    }

    public bool IsJetpackEnabled()
    {
        return m_jetpackEnabled;
    }

    public bool IsGravityEnabled()
    {
        return m_gravityEnabled;
    }

    public bool HasEnergyForJetpack()
    {
        return energy > jetpackEnergyUsage * Time.fixedDeltaTime;
    }

    public bool HasEnergyForGravity()
    {
        return energy > gravityEnergyUsage * Time.fixedDeltaTime;
    }

    // Tries to set enabled status to the specified value, and returns the status
    public bool EnableJetpackIfPossible(bool enabled)
    {
        if (!enabled)
        {
            m_jetpackEnabled = false; // We just want do disable it
            return false;
        }
        else if (!m_jetpackEnabled)   // No point in enabling if it's already on
        {
            if (m_jetpackNeedsCooldown)
            {
                return false; // Must wait till we have enough energy to start
            }
            else
            {
                if (energy < jetpackIgnitionEnergyUsage)
                {
                    return false; // We don't have enough energy to ignite
                }
                else
                {
                    energy -= jetpackIgnitionEnergyUsage; // Draw ignition energy
                    m_jetpackEnabled = true;
                    return true; // We can enable!
                }
            }
        }
        else // We want to enable but it is already enabled
        {
            return true;
        }
    }

    // Tries to set enabled status to the specified value, and returns the status
    public bool EnableGravityIfPossible(bool enabled)
    {
        if (!enabled)
        {
            m_gravityEnabled = false; // We just want do disable it
            return false;
        }
        else if (!m_gravityEnabled)   // No point in enabling if it's already on
        {
            if (m_gravityNeedsCooldown)
            {
                return false; // Must wait till we have enough energy to start
            }
            else
            {
                if (energy < gravityIgnitionEnergyUsage)
                {
                    return false; // We don't have enough energy to ignite
                }
                else
                {
                    energy -= gravityIgnitionEnergyUsage; // Draw ignition energy
                    m_gravityEnabled = true;
                    return true; // We can enable!
                }
            }
        }
        else // We want to enable but it is already enabled
        {
            return true;
        }
    }
}

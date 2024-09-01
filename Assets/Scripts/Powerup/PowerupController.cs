using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField]
    private PowerupType powerupType;

    public PowerupType getPowerupType()
    {
        return powerupType;
    }
}

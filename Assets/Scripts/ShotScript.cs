using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 10);
    }
}


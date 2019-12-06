using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{

    public Transform shotPrefab;

    public float shootingRate = 0.25f;

    private float shootCooldown;

    public Vector2 rollingLeft = new Vector2(-1, 0);

    public Vector2 rollingRight = new Vector2(1, 0);

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position;

            ProjectileMoveScript move = shotTransform.gameObject.GetComponent<ProjectileMoveScript>();
            Player player = GetComponent<Player>();
            if (player.facingRight)
            {
                move.direction = this.rollingRight;
            } else
            {
                move.direction = this.rollingLeft;
            }
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}

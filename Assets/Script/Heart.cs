using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Heart : MonoBehaviour
{

    [SerializeField] AudioClip lifePickUpSFX;
    [SerializeField] int pointsForLifePickup = 1;
    public bool alreadyPickedup = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!alreadyPickedup)
        {
            FindObjectOfType<GameSession>().AddToLives(pointsForLifePickup);
            AudioSource.PlayClipAtPoint(lifePickUpSFX, Camera.main.transform.position);
            alreadyPickedup = true;
        }

        Destroy(gameObject);
    }

}
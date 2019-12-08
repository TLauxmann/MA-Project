using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cupcake : MonoBehaviour
{

    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int pointsForCoinPickup = 10;
    public bool alreadyPickedup = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!alreadyPickedup)
        {
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            alreadyPickedup = true;
        }

        Destroy(gameObject);
    }

}
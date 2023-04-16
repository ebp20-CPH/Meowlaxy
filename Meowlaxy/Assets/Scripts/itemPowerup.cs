using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPowerup : MonoBehaviour
{

    public PowerupEffect powerupEffect;

    private void onTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc)
        {
            Destroy(gameObject);
            powerupEffect.Apply(collision.gameObject);
        }
    }
}
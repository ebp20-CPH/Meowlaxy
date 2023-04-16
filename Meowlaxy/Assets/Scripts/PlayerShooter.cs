using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] GameObject ProjectileStart;

    float triggerInput;

    float shootDelay = 0.5f;
    float shootTimer = 0f;

    public float projSpeed = 5f;

    GameObject tempProjectile;

    float projSpeedMod = 1f;
    float projRateMod = 1f;
    float projSizeMod = 1f;
    float projDamMod = 1f;
    float projHPMod = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShootUpdate();
    }

    // Logic for trigger input
    private void ShootUpdate() 
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0f)
            shootTimer = 0f;

        triggerInput = Input.GetAxis("RightTrigger");

        if (shootTimer == 0f && (triggerInput > 0f || Input.GetAxis("AltPitch") != 0f || Input.GetAxis("AltRoll") != 0f))
        {
            shootTimer = shootDelay / projRateMod;

            tempProjectile = Instantiate(ProjectilePrefab);
            tempProjectile.transform.position = ProjectileStart.transform.position;
            tempProjectile.transform.rotation = ProjectileStart.transform.rotation;
            tempProjectile.transform.localScale *= projSizeMod;

            Rigidbody2D rb = tempProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = ProjectileStart.transform.up * projSpeed * projSpeedMod;

            FindObjectOfType<AudioManager>().Play("Shoot");
        }
    }

    public void UpdateStats(float size, float speed, float rate, float damage, float hp) 
    {
        projSizeMod = size;
        projHPMod = hp;
        projDamMod = damage;
        projRateMod = rate;

        projSpeedMod = speed;
    }
}

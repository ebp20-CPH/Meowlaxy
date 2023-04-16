using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerShooter ps;

    Vector2 movement;
    
    Vector2 look;
    float lookAngle = 0f;

    int xpGoal = 1;
    int xpCounter = 0;

    private float BaseSpeed;
    [SerializeField] public float PlayerSpeed = 3f;
    [SerializeField] bool isLocked = false;

    float projectileDamageMod = 1f;
    float projectileHPMod = 1f;
    float projectileSizeMod = 1f; 
    float projectileSpeedMod = 1f; 
    float projectileFireRateMod = 1f;
    float playerSpeedMod = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        ps = GameObject.FindObjectOfType<PlayerShooter>();

        BaseSpeed = PlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked) 
        {
            PlayerMove();
            PlayerLook();
        }

        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape)) 
        {
            LevelManager lm = FindObjectOfType<LevelManager>();
            if (lm) 
            {
                lm.LoadScene(0);
            }
        }
    }

    private void PlayerMove() 
    {
        // Get controller/keyboard input
        movement = new Vector2(Input.GetAxis("Horizontal") * PlayerSpeed * playerSpeedMod * Time.deltaTime, Input.GetAxis("Vertical") * PlayerSpeed * playerSpeedMod * Time.deltaTime);

        // Affect player pos
        this.transform.Translate(movement, Space.World);

        this.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x, -16f, 16f), Mathf.Clamp(this.transform.position.y, -9f, 9f));
    }

    private void PlayerLook() 
    {
        // Get controller / mouse input
        look = new Vector2(Input.GetAxis("Pitch") + Input.GetAxisRaw("AltPitch"), -Input.GetAxis("Roll") + Input.GetAxisRaw("AltRoll"));

        // Normalize Angle
        if (look.x != 0 || look.y != 0f)
        {
            lookAngle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));
        }
    }

    public void LevelUp() 
    {
        float roll = Random.Range(0f, 1f);

        if (roll < .166f)
        {
            projectileDamageMod += 1f;
        }
        else if (roll < .333f)
        {
            projectileHPMod += 1f;
        }
        else if (roll < .5f)
        {
            projectileSizeMod += .2f;
        }
        else if (roll < .666f)
        {
            projectileSpeedMod += .15f;
        }
        else if (roll < .8333f)
        {
            projectileFireRateMod += .5f;
        }
        else 
        {
            playerSpeedMod += .2f;
        }

        ps.UpdateStats(projectileSizeMod, projectileSpeedMod, projectileFireRateMod, projectileDamageMod, projectileHPMod);

        Debug.Log("Leveled Up" + xpCounter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DropController dc = collision.GetComponent<DropController>();

        if (dc) 
        {
            // Add xp
            xpCounter++;
            LevelUp();

            // Destroy drop
            Destroy(dc.gameObject);
        }
    }
}

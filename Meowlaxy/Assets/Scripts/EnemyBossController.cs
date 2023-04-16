using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : EnemyController
{
    // Start is called before the first frame update
    public GameObject rotateAround;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject spawnPoint;

    float currentTime = 0f;
    float startingTime = 0f;

    private void Start()
    {
        rotateAround = GameObject.FindGameObjectWithTag("Planet");
        earth = rotateAround;

        hp = 250;
        speed = 1f;
    }

    private void SpawnEnemy()
    {
        GameObject en = Instantiate(EnemyPrefab);
        en.transform.position = spawnPoint.transform.position;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileController prc = collision.gameObject.GetComponent<ProjectileController>();
        if (prc)
        {

            hp--;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

    protected override void EnemyMove()
    {
        float d = Vector3.Magnitude(this.transform.position - rotateAround.transform.position);

        float angle = Mathf.Atan2(rotateAround.transform.position.y - this.transform.position.y, rotateAround.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (d > 10f)
        {
            transform.position = Vector2.MoveTowards(transform.position, earth.transform.position, speed * Time.deltaTime);
        }
        else if (d <= 10f)
        {
            transform.RotateAround(rotateAround.transform.position, Vector3.forward, speed * 2 * Time.deltaTime);

            currentTime += Time.deltaTime;
            if (currentTime >= 3)
            {
                SpawnEnemy();
                currentTime = startingTime;
            }
        }
    }

    public override int Damage(int d)
    {
        // Check if boss was killed
        if (hp - d <= 0)
        {
            // Call text
            //GameObject.FindGameObjectWithTag("FinalText").GetComponent<Dialogue>().StartText();
        }

        return base.Damage(d);
        
    }
}

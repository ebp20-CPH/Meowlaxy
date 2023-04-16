using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] GameObject ProjParticle;

    float timeToLive = 5f;
    public int hp = 1;
    public int damage = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        timeToLive -= Time.fixedDeltaTime;
        if (timeToLive <= 0f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();

        if (ec) 
        {
            int initHP = ec.GetHealth();
            hp -= initHP;

            int remainingHP = ec.Damage(damage);

            if (remainingHP > 0)
            {
                CreateParticle();
                Destroy(this.gameObject);
            }
            else 
            {
                ec.CreateDrop();
                Destroy(collision.gameObject);

                FindObjectOfType<AudioManager>().Play("Death");
            }

            if (hp <= 0f) 
            {
                CreateParticle();
                Destroy(this.gameObject);
            }
        }
    }

    private void CreateParticle() 
    {
        GameObject g = Instantiate(ProjParticle);
        g.transform.position = this.transform.position;
    }
}

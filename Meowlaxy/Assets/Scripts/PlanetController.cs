using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] GameObject ImpactParticle;

    public int hp = 10;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController en = collision.GetComponent<EnemyController>();

        if (en) 
        {
            hp--;
            SpawnImpact(collision.gameObject);
            Destroy(collision.gameObject);
            if (hp <= 0)
            {
                PlanetDeath();
            }
        }
    }

    private void PlanetDeath() 
    {

    }

    private void SpawnImpact(GameObject impactor) 
    {
        GameObject g = Instantiate(ImpactParticle);
        g.transform.position = impactor.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected GameObject drop;
    [SerializeField] protected GameObject earth;
    [SerializeField] protected float speed = 1.5f;
    public int hp = 1;

    protected bool randomRot = true;

    // Start is called before the first frame update
    void Start()
    {
        earth = GameObject.FindGameObjectWithTag("Planet");

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileController projc = collision.gameObject.GetComponent<ProjectileController>();
        if (projc)
        {

            hp--;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    protected virtual void EnemyMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, earth.transform.position, speed * Time.deltaTime);
    }

    public virtual int Damage(int d) 
    {
        hp -= d;
        return hp;
    }

    public void CreateDrop() 
    {
        GameObject g = Instantiate(drop);
        g.transform.position = this.transform.position;
    }

    public int GetHealth() 
    {
        return hp;
    }
}

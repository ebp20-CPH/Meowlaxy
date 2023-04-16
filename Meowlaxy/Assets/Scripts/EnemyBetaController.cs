using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBetaController : EnemyController
{
    Vector2 target;
    float currentTime = 0f;
    float startingTime = 5f;

    int randomCounter = 3;

    private void LateAwake()
    {
        speed = 2f;
        hp = 2;

        currentTime = startingTime;
        target = earth.transform.position;
    }

    protected override void EnemyMove()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 && randomCounter > 0)
        {
            target = earth.transform.position;
            target.x += Random.Range(-10f, 10f);
            target.y += Random.Range(-10f, 10f);
            currentTime = startingTime;

            randomCounter--;
        }
        else if (currentTime <= 0 && randomCounter <= 0) 
        {
            target = earth.transform.position;
            currentTime = 100f;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }
}

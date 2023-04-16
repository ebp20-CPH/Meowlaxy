using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    GameObject star;
    [SerializeField] float vertMargin = 5f;
    [SerializeField] float horizMargin = 10f;
    [SerializeField] float starSpeed = 3f;

    private void Start()
    {
        star = this.gameObject;
        star.transform.position = new Vector2(Random.Range(-horizMargin, horizMargin), Random.Range(-vertMargin, vertMargin));

        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        if (rb) 
        {
            rb.velocity = new Vector2(-starSpeed * Random.Range(.4f, 1f), 0);
        }
    }

    private void Update()
    {
        if (transform.position.x < -horizMargin) 
        {
            // Send to right
            transform.position = new Vector3(horizMargin, Random.Range(-vertMargin, vertMargin), 0);
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.velocity = new Vector2(-starSpeed * Random.Range(.4f, 1f), 0);
            }
        }
    }
}

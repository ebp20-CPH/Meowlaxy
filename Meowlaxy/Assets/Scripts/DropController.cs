using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    public bool picked = false;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (pc && !picked) 
        {
            picked = true;
            player = collision.gameObject;
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
    }
}

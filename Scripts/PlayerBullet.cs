using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the bullet's current position
        Vector2 position = transform.position;

        //Compute the bullet's new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update the bullet's position
        transform.position = position;

        //This is the top right of the screen 
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //If the bullet goes out of frame, destroy it
        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision between the player's ship and the enemy's bullet
        if (col.tag == "EnemyShipTag")
        {
            Destroy(gameObject); //Destroy the enemy's bullet
        }
    }
}

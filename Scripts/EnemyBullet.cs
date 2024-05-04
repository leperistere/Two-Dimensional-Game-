using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed;
    Vector2 _direction;
    bool isReady;//To know when the bullet direction is set

    void Awake()
    {
        isReady = false;
        speed = 5f;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    //Function to set the bullet's direction
    public void SetDirection(Vector2 direction)
    {
        //Set the direction to normalized to get a unit vector
        _direction = direction.normalized;

        isReady = true; //flag = true
    }

    // Update is called once per frame
    void Update()
    {
        if(isReady)
        {
            //Get the bullet's current position
            Vector2 position = transform.position;

            //Calculate the new position
            position += _direction * speed * Time.deltaTime;
            transform.position = position;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));//this is the bottom left point of the screen
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));//this is the top right point of the screen

            if((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision between the player's ship and the enemy's bullet
        if (col.tag == "PlayerShipTag")
        {
            Destroy(gameObject); //Destroy the enemy's bullet
        }
    }
}

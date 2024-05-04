using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    GameObject scoreUITextGO;//Reference to the text UI game object
    float speed;

    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;

        scoreUITextGO = GameObject.FindGameObjectWithTag("TextScoreTag");
    }

    // Update is called once per frame
    void Update()
    {
        //Get the enemy's current position
        Vector2 position = transform.position;

        //Calculate the new position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //Update the enemy's position
        transform.position = position;

        //This is the bottom left of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //If the enemy gets out of frame, destroy it
        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision between the enemy's ship and either with the player's ship or the player's bullet
        if (col.tag == "PlayerBulletTag" || col.tag == "PlayerShipTag")
        {
            PlayExplosion();

            //add 100 points to the score
            scoreUITextGO.GetComponent<GameScore>().Score += 100;

            Destroy(gameObject); //Destroy the enemy's ship
        }
    }

    //Instantiate the enemy's explosion
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }
}

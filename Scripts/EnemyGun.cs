using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        //Fire bullets per second
        Invoke("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null)//If the player is still alive
        {
            //Instantiate the enemy's bullet
            GameObject bullet = (GameObject)Instantiate(EnemyBullet);

            //Set the bullet's initial position
            bullet.transform.position = transform.position;

            //Calculate the bullet's direction towards the player
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            //Set the bullet's direction
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}

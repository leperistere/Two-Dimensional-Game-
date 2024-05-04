using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //Get the current position
        Vector2 position = transform.position;
        //Create a new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        //Update the star position
        transform.position = position;
        //Bottom-left on the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //Top-right on the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //If the star goes beyond the screen, then posiition
        //it at the top edge of the screen and randomly in between
        //the right and left side of the screen

        if(transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x),max.y);
        }

    }
}

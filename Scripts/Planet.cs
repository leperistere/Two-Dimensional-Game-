using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed;
    public bool isMoving;

    Vector2 min;
    Vector2 max;

    void Awake()
    {
        isMoving = false;

        //Bottom-left on the screen
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //Top-right on the screen
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //Add the planet sprite half height to max y
        max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        //Subtract the planet sprite half height to min y
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (!isMoving)
            return;

        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        transform.position = position;

        if(transform.position.y < min.y)
        {
            isMoving = false;
        }
        
    }

    public void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}

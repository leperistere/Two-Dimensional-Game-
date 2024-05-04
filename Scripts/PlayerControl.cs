using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject GManager;
    public float speed;
    public GameObject PlayerBullet;//The bullet prefab
    public GameObject BulletPosition1;
    public GameObject BulletPosition2;
    public GameObject Explosion;//The explosion prefab
    public TextMeshProUGUI LivesUI;//Referece to the lives text
    public GameObject HintTextGO;
    public GameObject HintBox;

    public static bool displayHint = false;
    public int hintNum;
    List<string> GameScoreHints = new List<string> { "The more time you play,the more chances you have to increase the score!", "Shoot the enemies to stay alive for longer.", "Move your player constantly to avoid enemy fire" };

    const int MaxLives = 3; //Lives of the player
    int lives;
    // Start is called before the first frame update

    public void Init()
    {
        lives = MaxLives;
        //Update lives
        LivesUI.text = lives.ToString();

        //Reset the player's position to the center of the screen
        transform.position = new Vector2(0, 0);

        //Set the game object active
        gameObject.SetActive(true);
    }
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {
        //Fire a bullet when the spacebar is pressed
        if(Input.GetKeyDown("space"))
        {

            //Play the sound effect
            GetComponent<AudioSource>().Play();
            //Instantiate the first bullet
            GameObject bullet1 = (GameObject)Instantiate(PlayerBullet);
            bullet1.transform.position = BulletPosition1.transform.position;//Set the bullet's initial position

            //Instantiate the second bullet
            GameObject bullet2 = (GameObject)Instantiate(PlayerBullet);
            bullet2.transform.position = BulletPosition2.transform.position;//Set the bullet's initial position
        }
        float x = Input.GetAxisRaw("Horizontal");//the value will be -1,0 and 1 (for left,no input and right)
        float y = Input.GetAxisRaw("Vertical");//the value will be -1,0 and 1 (for down,no input and up)

        //now based on the input we compute a direction vector and we normalize it to get a unit vector.
        Vector2 direction = new Vector2 (x, y).normalized;

        //we call the function that computes and sets the player's position
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        //Find the screen limits to the player's movements.
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));//this is the bottom left point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1, 1));//this is the top right point of the screen

        max.x = max.x - 0.225f; //subtract the player sprite half width
        min.x = min.x + 0.225f; //add the player sprite half width

        max.y = max.y - 0.285f; //subtract the player sprite half height
        min.y = min.y + 0.285f; //add the player sprite half height

        //Get the player's current position
        Vector2 pos = transform.position;

        //Calculate the new position
        pos += direction * speed * Time.deltaTime;

        //Make sure the position is not outside the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Update the player's position
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision between the player's ship and either with the enemy's ship or the enemy's bullet
        if(col.tag == "EnemyShipTag" || col.tag == "EnemyBulletTag")
        {
            PlayExplosion();

            lives--;
            LivesUI.text = lives.ToString();

            Text text = HintTextGO.GetComponent<Text>();
            //Show a random hint on the screen to help the player 
            if (displayHint == false)
            {
                HintBox.SetActive(true);
                HintTextGO.SetActive(true);
                displayHint = true;
                hintNum = Random.Range(1, 3);
                text.text = GameScoreHints[hintNum];
                Invoke("hintGen", 5f);
            }

            if (lives == 0)
            {
                //Change the game manager state to game over
                GManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                //Hide the players ship
                gameObject.SetActive(false);
            }
        }
    }

    void hintGen()
    {
        HintBox.SetActive(false);
        HintTextGO.SetActive(false);
        displayHint = false;
    }

    //Instantiate the player's explosion
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }
}

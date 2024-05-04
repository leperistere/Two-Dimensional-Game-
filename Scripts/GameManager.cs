using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimerCounterGO;
    public GameObject GameTitleGO;
    public GameObject HintTextGO;
    public GameObject HintBox;

    public static bool displayHint = false;
    public int hintNum;
    List<string> GameOverHints = new List<string> { "Moving continuously can help you avoid enemy fire",
        "Try firing more often to kill enemies", "Position is key!Stay low on the screen to help you with aiming!",
        "The faster you hit the fire button the more bullets you shoot","Avoid going into enemies,you will lose lives",
        "Use the arrows or the AWSD buttons to move your spaceship"};


    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMstate;

    // Start is called before the first frame update
    void Start()
    {
        GMstate = GameManagerState.Opening;
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch(GMstate)
        {
            case GameManagerState.Opening:

                GameOverGO.SetActive(false);

                //Display Game Title
                GameTitleGO.SetActive(true);
                //Set play button visible.
                playButton.SetActive(true);

                HintBox.SetActive(false);
                HintTextGO.SetActive(false);
                displayHint = false;

                break;
            case GameManagerState.Gameplay:

                //Reset the score
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                //Hide the play buttonand gameover logo on gameplay state
                playButton.SetActive(false);
                GameOverGO.SetActive(false);

                //Hide the Game Title
                GameTitleGO.SetActive(false);

                //Set the player visible and init the player lives
                playerShip.GetComponent<PlayerControl>().Init();

                //Start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleSpawner();

                //Start the Timer
                TimerCounterGO.GetComponent<TimerCounter>().StartTimeCounter();

                    break;
            case GameManagerState.GameOver:

                //Stop the counter
                TimerCounterGO.GetComponent<TimerCounter>().StopTimeCounter();
                //Stop enemy spawn
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleSpawner();
                //Show the gameover logo
                GameOverGO.SetActive(true);

                Text text = HintTextGO.GetComponent<Text>();
                //Show a random hint on the screen to help the player 
                if (displayHint == false)
                {
                    HintBox.SetActive(true);
                    HintTextGO.SetActive(true);
                    displayHint = true;
                    hintNum = Random.Range(1, 6);
                    text.text = GameOverHints[hintNum];
                }

                //Change the GameManager state to Opening after 8 seconds
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    //Setting Game Manager State
    public void SetGameManagerState(GameManagerState state)
    {
        GMstate = state;
        UpdateGameManagerState();
    }

    //Function for when the button is clicked
    public void StartGamePlay()
    {
        GMstate = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}

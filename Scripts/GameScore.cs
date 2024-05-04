using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Text scoreTextUI;
    public GameObject HintTextGO;
    public GameObject HintBox;

    public static bool displayHint = false;
    public int hintNum;
    List<string> GameScoreHints = new List<string> { "When you kill enemies, the score increases","Try firing more often to kill enemies", "Keep it up you are doing great!","The faster you hit the fire button the more bullets you shoot"};

    int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateScoreTextUI();
        }
    }
    //Use Start for initialization
    void Start()
    {
        //Get the Text UI component of this GameObject
        scoreTextUI = GetComponent<Text>();
    }

    // Update is called once per frame
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:0000000}", score);
        scoreTextUI.text = scoreStr;
        int sum = 0;
        Text text = HintTextGO.GetComponent<Text>();
        //Show a random hint on the screen to help the player 
        if (displayHint == false && score > sum + 500)
        {
            HintBox.SetActive(true);
            HintTextGO.SetActive(true);
            displayHint = true;
            hintNum = Random.Range(1, 3);
            text.text = GameScoreHints[hintNum];
            Invoke("hintGen", 5f);
            sum = sum + 500;
        }
    }

    void hintGen()
    {
        HintBox.SetActive(false);
        HintTextGO.SetActive(false);
        displayHint = false;
    }
}

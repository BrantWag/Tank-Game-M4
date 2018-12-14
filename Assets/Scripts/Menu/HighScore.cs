using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public RectTransform highScoreLoc;
    public Text highScoreNum;
    public Vector3 singlePlayerPos = new Vector3(-767, 390, 0);
    public Vector3 multiPlayerPos = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        // set the location of the high score depending on the multiplayer mode
        highScoreLoc = GetComponent<RectTransform>();
        if (GameManager.instance.isMultiplayer == true)
        {
            setHighScoreMultiplayer();
        }
        else
        {
            setHighScoreSinglePlayer();
        }
        highScoreNum.text = GameManager.instance.highScore.ToString();
    }

    public void setHighScoreSinglePlayer()
    {
        highScoreLoc.anchoredPosition = singlePlayerPos;
    }

    public void setHighScoreMultiplayer()
    {
        highScoreLoc.anchoredPosition = multiPlayerPos;
    }
    // display high score
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.highScore > int.Parse(highScoreNum.text.ToString()))
        {
            highScoreNum.text = GameManager.instance.highScore.ToString();
        }
    }
}

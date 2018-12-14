using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    public TankData data;

    // Use this for initialization
    void Start()
    {
        score = GetComponent<Text>();
        data = GetComponentInParent<TankData>();
        OverLayScore();
    }

    // Update is called once per frame
    void Update()
    {
        OverLayScore();
    }

    // set score to the players current score
    void OverLayScore()
    {
        score.text = data.score.ToString();
    }
}
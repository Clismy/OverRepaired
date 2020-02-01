using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreBored : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public int Score;
    private Text pointText;
    private int oldScore;
    void Start()
    {
        pointText = GetComponent<Text>();
        Score = 0;
    }

    public void IncreaseScore()
    {
        Score++;
    }
    void ChangeText()
    {
        pointText.text = Score.ToString();
        oldScore = Score;
    }

    // Update is called once per frame
    void Update()
    {
        if(Score != oldScore)
        {
            ChangeText();
        }
    }
}

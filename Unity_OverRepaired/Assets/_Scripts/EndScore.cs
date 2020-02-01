using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndScore : MonoBehaviour
{
    public Image endScreen;
    public Text endScore;
    public ScoreBored scoreBored;
    public float fadeInTime;
    private float time;

    private void Start()
    {
        fadeIn(endScore);
        fadeIn(endScreen);
        gameObject.SetActive(false);
    }

    public void fadeIn(Graphic graphic)
    {
        var tempColor = graphic.color;
        tempColor.a = Mathf.Lerp(0f, fadeInTime, time/fadeInTime) / fadeInTime;
        graphic.color = tempColor;

    }

    void Score()
    {
        endScore.text = "Your Created " + scoreBored.Score + " Robots";
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < fadeInTime)
        {
            fadeIn(endScore);
            fadeIn(endScreen);
            Score();
        }
    }
}

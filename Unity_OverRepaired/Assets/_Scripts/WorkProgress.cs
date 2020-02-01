using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorkProgress : MonoBehaviour
{
    private RectTransform rt;
    private RectTransform canvas;
   

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }


    public void Working(RepairStation repairStation)
    {
        if (repairStation.timer >= repairStation.workTime){
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        //transform.position = Camera.main.WorldToViewportPoint(player.transform.position);
        var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, repairStation.gameObject.transform.position);
        screenPos.Scale(new Vector2(1f, 1.2f));
        rt.anchoredPosition = screenPos - canvas.sizeDelta / 2f;

        gameObject.GetComponent<Image>().fillAmount = repairStation.timer / repairStation.workTime;
        
    }
}
